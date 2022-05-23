namespace Movies.Common.Wrappers;

public class PaginatedResponse<T>
{
    private PaginatedResponse(short pageIndex, short pageSize, short count, IEnumerable<T> items)
    {
        PageIndex = pageIndex;
        TotalPages = (short)Math.Ceiling(count / (double)pageSize);
        TotalCount = count;
        Items = items;
    }

    
    public short PageIndex { get; }
    public short TotalPages { get; }
    public short TotalCount { get; }
    public IEnumerable<T> Items { get; }
    public bool HasPreviousPage => PageIndex > 1;
    public bool HasNextPage => PageIndex < TotalPages;
    
    
    public static async Task<PaginatedResponse<T>> CreateAsync(IQueryable<T> source, short pageIndex, short pageSize)
    {
        var count = (short)await source.CountAsync();
        var items = await source
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedResponse<T>(pageIndex, pageSize, count, items);
    }
}
