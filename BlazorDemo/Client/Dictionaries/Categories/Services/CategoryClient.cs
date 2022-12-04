using BlazorDemo.Client.Shared.Services;
using BlazorDemo.Client.Shared.Types;
using BlazorDemo.Shared.Dictionaries.Categories;

namespace BlazorDemo.Client.Dictionaries.Categories.Services;

public interface ICategoryClient
{
    Task<ApiResult<List<CategoryDto>>> GetAll();
}

public class CategoryClient : ClientBase, ICategoryClient
{
    public CategoryClient(HttpClient client, INotifier notifier) : base(client, notifier)
    {
    }

    public async Task<ApiResult<List<CategoryDto>>> GetAll()
        => await Get<List<CategoryDto>>("api/categories");
}