namespace BlazorDemo.Shared;

public class PagedResponse<T>
{
    public IList<T> Items { get; set; } = new List<T>();
    public int CurrentPage { get; set; }
    public int PageCount { get; set; }
    public int PageSize { get; set; }
    public int RowCount { get; set; }
}
