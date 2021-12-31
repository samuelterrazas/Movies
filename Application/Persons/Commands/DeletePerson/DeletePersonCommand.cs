using MediatR;
using Movies.Domain.Entities;
using Movies.Application.Common.Exceptions;
using Movies.Application.Common.Interfaces;

namespace Movies.Application.Persons.Commands.DeletePerson;

public class DeletePersonCommand : IRequest
{
    public int Id { get; set; }
}

public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public DeletePersonCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
        
    public async Task<Unit> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        var person = await _dbContext.Persons.FindAsync(request.Id);

        if (person is null)
            throw new NotFoundException(nameof(Person), request.Id);

        _dbContext.Persons.Remove(person);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
