using BlazorDemo.Client.Shared.Services;
using BlazorDemo.Client.Shared.Types;
using BlazorDemo.Shared;
using BlazorDemo.Shared.Results;
using BlazorDemo.Shared.Todos;

namespace BlazorDemo.Client.Todos.Services;

public interface ITodoClient
{
    Task<ApiResult<PagedResponse<TodoDto>>> Search(int pageNumber, int pageSize);
    Task<ApiResult<int>> GetOverdueCount();
    Task<ApiResult<IdResponse>> Create(TodoCreateDto dto);
    Task<ApiResult<Success>> Complete(string id);
    Task<ApiResult<Success>> Delete(string id);
}

public class TodoClient : ClientBase, ITodoClient
{
    public TodoClient(HttpClient client, INotifier notifier) : base(client, notifier)
    {
    }

    public async Task<ApiResult<IdResponse>> Create(TodoCreateDto dto)
        => await Post<TodoCreateDto, IdResponse>("api/todos", dto);

    public async Task<ApiResult<PagedResponse<TodoDto>>> Search(int pageNumber, int pageSize)
        => await Get<PagedResponse<TodoDto>>($"api/todos/search?pageNumber={pageNumber}&pageSize={pageSize}");

    public async Task<ApiResult<int>> GetOverdueCount()
        => await Get<int>("api/todos/overdueCount");

    public async Task<ApiResult<Success>> Complete(string id)
        => await Post<Success>($"api/todos/{id}");

    public async Task<ApiResult<Success>> Delete(string id)
        => await Delete<Success>($"api/todos/{id}");
}
