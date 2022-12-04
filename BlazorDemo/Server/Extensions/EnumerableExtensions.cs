using BlazorDemo.Shared;

namespace BlazorDemo.Server.Extensions;

public static class EnumerableExtensions
{
    public static PagedResponse<T> ToPagedResponse<T>(this IEnumerable<T> query, int page, int pageSize)
    {
        var result = new PagedResponse<T>
        {
            CurrentPage = page,
            PageSize = pageSize,
            RowCount = query.Count()
        };

        var pageCount = (double)result.RowCount / pageSize;
        result.PageCount = (int)Math.Ceiling(pageCount);

        var skip = (page - 1) * pageSize;
        result.Items = query.Skip(skip).Take(pageSize).ToList();

        return result;
    }
}
