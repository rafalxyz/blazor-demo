using System.Text;
using System.Text.Json;
using BlazorDemo.Client.Shared.Extensions;
using BlazorDemo.Client.Shared.Types;

namespace BlazorDemo.Client.Shared.Services;

public class ClientBase
{
    private readonly HttpClient _client;
    private readonly INotifier _notifier;

    public ClientBase(HttpClient client, INotifier notifier)
    {
        _client = client;
        _notifier = notifier;
    }

    protected async Task<ApiResult<TResponse>> Get<TResponse>(string path)
    {
        var response = await _client.GetAsync(path);
        var result = await response.ToApiResult<TResponse>();
        result.OnError(error => _notifier.ShowError(error.Detail));
        return result;
    }

    protected async Task<ApiResult<TResponse>> Post<TRequest, TResponse>(string path, TRequest data)
    {
        var response = await _client.PostAsync(path, new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json"));
        var result = await response.ToApiResult<TResponse>();
        result.OnError(error => _notifier.ShowError(error.Detail));
        return result;
    }

    protected async Task<ApiResult<TResponse>> Post<TResponse>(string path)
    {
        var response = await _client.PostAsync(path, null);
        var result = await response.ToApiResult<TResponse>();
        result.OnError(error => _notifier.ShowError(error.Detail));
        return result;
    }

    protected async Task<ApiResult<TResponse>> Put<TRequest, TResponse>(string path, TRequest data)
    {
        var response = await _client.PutAsync(path, new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json"));
        var result = await response.ToApiResult<TResponse>();
        result.OnError(error => _notifier.ShowError(error.Detail));
        return result;
    }

    protected async Task<ApiResult<TResponse>> Delete<TResponse>(string path)
    {
        var response = await _client.DeleteAsync(path);
        var result = await response.ToApiResult<TResponse>();
        result.OnError(error => _notifier.ShowError(error.Detail));
        return result;
    }
}
