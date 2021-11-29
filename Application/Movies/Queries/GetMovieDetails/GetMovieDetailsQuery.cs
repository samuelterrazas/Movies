using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Movies.Application.Common.Exceptions;
using Movies.Application.Common.Interfaces;
using Movies.Application.Movies.DTOs;
using Movies.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.Application.Movies.Queries.GetMovieDetails
{
    public class GetMovieDetailsQuery : IRequest<MovieDetailsDto>
    {
        public int Id { get; set; }
    }

    public class GetMovieDetailsQueryHandler : IRequestHandler<GetMovieDetailsQuery, MovieDetailsDto>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetMovieDetailsQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<MovieDetailsDto> Handle(GetMovieDetailsQuery request, CancellationToken cancellationToken)
        {
            var movie = await _dbContext.Movies
                .AsNoTracking()
                .Include(movie => movie.MovieGenres).ThenInclude(movieGenre => movieGenre.Genre)
                .Include(movie => movie.MoviePersons).ThenInclude(moviePerson => moviePerson.Person)
                .FirstOrDefaultAsync(i => i.Id.Equals(request.Id), cancellationToken);

            if (movie is null)
                throw new NotFoundException(nameof(Movie), request.Id);

            return _mapper.Map<MovieDetailsDto>(movie);
        }
    }
}
