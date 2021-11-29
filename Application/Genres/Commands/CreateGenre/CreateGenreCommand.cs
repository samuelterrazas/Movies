using AutoMapper;
using MediatR;
using Movies.Application.Common.Interfaces;
using Movies.Application.Common.Mappings;
using Movies.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.Application.Genres.Commands.CreateGenre
{
    public class CreateGenreCommand : IRequest<int>, IMapFrom<Genre>
    {
        public string Name { get; set; }
    }

    public class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand, int>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateGenreCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = _mapper.Map<Genre>(request);

            _dbContext.Genres.Add(genre);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return genre.Id;
        }
    }
}
