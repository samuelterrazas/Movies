using Movies.Application.Common.Wrappers;

namespace Movies.Application.Common.Extensions
{
    public static class QueryableExtension
    {
        public static Task<PaginatedResponse<TDestination>> PaginatedResponseAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize)
            => PaginatedResponse<TDestination>.CreateAsync(queryable, pageNumber, pageSize);
    }
}
