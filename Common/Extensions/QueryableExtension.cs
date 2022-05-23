namespace Movies.Common.Extensions;

public static class QueryableExtension
{
    public static Task<PaginatedResponse<TDestination>> PaginatedResponseAsync<TDestination>(this IQueryable<TDestination> queryable, short pageNumber, short pageSize)
        => PaginatedResponse<TDestination>.CreateAsync(queryable, pageNumber, pageSize);
}