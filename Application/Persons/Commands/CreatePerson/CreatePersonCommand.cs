using AutoMapper;
using MediatR;
using Movies.Domain.Entities;
using Movies.Application.Common.Interfaces;
using Movies.Application.Common.Mappings;

namespace Movies.Application.Persons.Commands.CreatePerson;

public class CreatePersonCommand : IRequest<int>, IMapFrom<Person>
{
    public string FullName { get; set; }
}

public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, int>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreatePersonCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
        
    public async Task<int> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        var person = _mapper.Map<Person>(request);

        _dbContext.Persons.Add(person);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return person.Id;
    }
}
