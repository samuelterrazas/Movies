using Movies.Application.Common.Wrappers;

namespace Movies.Application.Common.Mappings
{
    public static class MappingExtensions
    {
        public static Task<PaginatedResponse<TDestination>> PaginatedResponseAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize)
            => PaginatedResponse<TDestination>.CreateAsync(queryable, pageNumber, pageSize);
    }
}
