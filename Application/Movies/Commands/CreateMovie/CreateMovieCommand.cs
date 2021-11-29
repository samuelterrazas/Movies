using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Movies.Application.Common.Interfaces;
using Movies.Application.Movies.DTOs;
using Movies.Domain.Entities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Movies.Application.Common.Helpers;
using Movies.Application.Common.Mappings;
using Movies.Domain.Enums;

namespace Movies.Application.Movies.Commands.CreateMovie
{
    public class CreateMovieCommand : IRequest<int>, IMapFrom<Movie>
    {
        public string Title { get; set; }
        public int Release { get; set; }
        public string Duration { get; set; }
        public string MaturityRating { get; set; }
        public string Summary { get; set; }
        public IFormFile Image { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> Genres { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<MoviePersonDto>>))]
        public List<MoviePersonDto> Persons { get; set; }
        
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateMovieCommand, Movie>()
                .ForMember(movie => movie.MovieGenres, options => options.MapFrom(MovieGenres))
                .ForMember(movie => movie.MoviePersons, options => options.MapFrom(MoviePersons));
        }

        private IEnumerable<MovieGenre> MovieGenres(CreateMovieCommand command, Movie movie) =>
            (from genreId in command.Genres 
                select new MovieGenre
                {
                    GenreId = genreId
                }
            ).ToList();

        private IEnumerable<MoviePerson> MoviePersons(CreateMovieCommand command, Movie movie) =>
            (from moviePersonDto in command.Persons
                select new MoviePerson
                {
                    PersonId = moviePersonDto.PersonId, 
                    Role = (Role)moviePersonDto.Role, 
                    Order = moviePersonDto.Order
                }
            ).ToList();
    }

    public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, int>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IFileStore _fileStore;
        private const string Container = "movies";

        public CreateMovieCommandHandler(IApplicationDbContext dbContext, IMapper mapper, IFileStore fileStore)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _fileStore = fileStore;
        }

        public async Task<int> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
        {
            var movie = _mapper.Map<Movie>(request);

            if (request.Image is not null)
            {
                await using var memoryStream = new MemoryStream();
                await request.Image.CopyToAsync(memoryStream, cancellationToken);

                var content = memoryStream.ToArray();
                var extension = Path.GetExtension(request.Image.FileName);

                movie.Image = await _fileStore.SaveFile(content, extension, Container, request.Image.ContentType);
            }

            _dbContext.Movies.Add(movie);
            await _dbContext.SaveChangesAsync(cancellationToken);
                
            return movie.Id;
        }
    }
}
