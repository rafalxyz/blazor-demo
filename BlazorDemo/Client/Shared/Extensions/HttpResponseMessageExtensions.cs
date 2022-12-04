using System.Net;
using System.Text.Json;
using BlazorDemo.Client.Shared.Types;

namespace BlazorDemo.Client.Shared.Extensions;

public static class HttpResponseMessageExtensions
{
    public static async Task<ApiResult<T>> ToApiResult<T>(this HttpResponseMessage message)
    {
        var serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        var content = await message.Content.ReadAsStringAsync();

        if (!message.IsSuccessStatusCode)
        {
            return ApiResult<T>.Failure(JsonSerializer.Deserialize<ProblemDetails>(content, serializerOptions)!);
        }

        if (message.StatusCode == HttpStatusCode.NoContent)
        {
            return ApiResult<T>.Success((T)Activator.CreateInstance(typeof(T))!);
        }

        return ApiResult<T>.Success(JsonSerializer.Deserialize<T>(content, serializerOptions)!);
    }
}
