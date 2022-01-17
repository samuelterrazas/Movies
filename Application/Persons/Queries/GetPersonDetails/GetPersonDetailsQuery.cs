namespace Movies.Application.Persons.Queries.GetPersonDetails;

public record GetPersonDetailsQuery(int Id) : IRequest<PersonDetailsDto>;

public class GetPersonDetailsQueryHandler : IRequestHandler<GetPersonDetailsQuery, PersonDetailsDto>
{
    private readonly IApplicationDbContext _dbContext;

    public GetPersonDetailsQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PersonDetailsDto> Handle(GetPersonDetailsQuery request, CancellationToken cancellationToken)
    {
        var person = await _dbContext.Persons
            .AsNoTracking()
            .Include(p => p.MoviePersons)
                .ThenInclude(moviePerson => moviePerson.Movie)
                    .ThenInclude(movie => movie.Files)
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (person is null)
            throw new NotFoundException(nameof(Person), request.Id);

        return (PersonDetailsDto)person;
    }
}