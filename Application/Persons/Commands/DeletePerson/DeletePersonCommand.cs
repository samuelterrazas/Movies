namespace Movies.Application.Persons.Commands.DeletePerson;

public record DeletePersonCommand(int Id) : IRequest;


public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand>
{
    private readonly IApplicationDbContext _dbContext;

    
    public DeletePersonCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
     
    
    public async Task<Unit> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        var person = await _dbContext.Persons.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (person is null)
            throw new NotFoundException(nameof(Person), request.Id);

        _dbContext.Persons.Remove(person);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
