using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Movies.Application.Common.Exceptions;
using Movies.Application.Common.Interfaces;
using Movies.Application.Persons.DTOs;
using Movies.Domain.Entities;

namespace Movies.Application.Persons.Queries.GetPersonDetails;

public class GetPersonDetailsQuery : IRequest<PersonDetailsDto>
{
    public int Id { get; set; }
}

public class GetPersonDetailsQueryHandler : IRequestHandler<GetPersonDetailsQuery, PersonDetailsDto>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetPersonDetailsQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
        
    public async Task<PersonDetailsDto> Handle(GetPersonDetailsQuery request, CancellationToken cancellationToken)
    {
        var person = await _dbContext.Persons
            .AsNoTracking()
            .Include(p => p.MoviePersons).ThenInclude(moviePerson => moviePerson.Movie)
            .FirstOrDefaultAsync(p => p.Id.Equals(request.Id), cancellationToken);

        if (person is null)
            throw new NotFoundException(nameof(Person), request.Id);

        return _mapper.Map<PersonDetailsDto>(person);
    }
}