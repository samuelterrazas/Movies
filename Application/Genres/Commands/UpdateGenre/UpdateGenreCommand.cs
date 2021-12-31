using AutoMapper;
using MediatR;
using Movies.Domain.Entities;
using Movies.Application.Common.Exceptions;
using Movies.Application.Common.Interfaces;
using Movies.Application.Common.Mappings;

namespace Movies.Application.Genres.Commands.UpdateGenre;

public class UpdateGenreCommand : IRequest, IMapFrom<Genre>
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class UpdateGenreCommandHandler : IRequestHandler<UpdateGenreCommand>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public UpdateGenreCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
        
    public async Task<Unit> Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
    {
        var genre = await _dbContext.Genres.FindAsync(request.Id);

        if (genre is null)
            throw new NotFoundException(nameof(Genre), request.Id);

        _mapper.Map(request, genre);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
