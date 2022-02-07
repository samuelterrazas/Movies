namespace Movies.Application.Persons.Queries.GetPersons;

public record GetPersonsQuery(int? PageNumber, int? PageSize, string Name) : IRequest<PaginatedResponse<PersonsDto>>;

public class GetPersonsQueryHandler : IRequestHandler<GetPersonsQuery, PaginatedResponse<PersonsDto>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetPersonsQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
        
    public async Task<PaginatedResponse<PersonsDto>> Handle(GetPersonsQuery request, CancellationToken cancellationToken)
    {
        var (pageNumber, pageSize, name) = request;
        
        var persons = _dbContext.Persons.AsQueryable();

        if (!string.IsNullOrWhiteSpace(name))
            persons = persons.Where(p => p.FullName.Contains(name));
            
        return await persons
            .AsNoTracking()
            .Select(person => (PersonsDto)person)
            .PaginatedResponseAsync(pageNumber ?? 1, pageSize ?? 10);
    }
}
