namespace Movies.Application.Persons.Commands.UpdatePerson;

public record UpdatePersonCommand(int Id, string FullName) : IRequest;


public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand>
{
    private readonly IApplicationDbContext _dbContext;

    
    public UpdatePersonCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    
    public async Task<Unit> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        var person = await _dbContext.Persons.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (person is null)
            throw new NotFoundException(nameof(Person), request.Id);
        
        person.FullName = request.FullName;
        
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
