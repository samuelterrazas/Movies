namespace Movies.Application.Persons.Commands.CreatePerson;

public record CreatePersonCommand(string FullName) : IRequest<int>;

public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, int>
{
    private readonly IApplicationDbContext _dbContext;

    public CreatePersonCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
        
    public async Task<int> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        var person = new Person {FullName = request.FullName};

        _dbContext.Persons.Add(person);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return person.Id;
    }
}
