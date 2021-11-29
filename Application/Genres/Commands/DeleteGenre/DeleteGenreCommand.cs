using MediatR;
using Movies.Application.Common.Exceptions;
using Movies.Application.Common.Interfaces;
using Movies.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.Application.Genres.Commands.DeleteGenre
{
    public class DeleteGenreCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteGenreCommandHandler : IRequestHandler<DeleteGenreCommand>
    {
        private readonly IApplicationDbContext _dbContext;

        public DeleteGenreCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<Unit> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = await _dbContext.Genres.FindAsync(request.Id);

            if (genre is null)
                throw new NotFoundException(nameof(Genre), request.Id);

            _dbContext.Genres.Remove(genre);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
