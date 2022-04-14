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
        var persons = _dbContext.Persons.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Name))
            persons = persons.Where(p => p.FullName.Contains(request.Name));
            
        return await persons
            .AsNoTracking()
            .Select(person => (PersonsDto)person)
            .PaginatedResponseAsync(request.PageNumber ?? 1, request.PageSize ?? 10);
    }
}
