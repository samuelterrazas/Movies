using AutoMapper;
using MediatR;
using Movies.Domain.Entities;
using Movies.Application.Common.Exceptions;
using Movies.Application.Common.Interfaces;
using Movies.Application.Common.Mappings;

namespace Movies.Application.Persons.Commands.UpdatePerson;

public class UpdatePersonCommand : IRequest, IMapFrom<Person>
{
    public int Id { get; set; }
    public string FullName { get; set; }
}

public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public UpdatePersonCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
        
    public async Task<Unit> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        var person = await _dbContext.Persons.FindAsync(request.Id);

        if (person is null)
            throw new NotFoundException(nameof(Person), request.Id);

        _mapper.Map(request, person);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
