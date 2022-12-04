using BlazorDemo.Shared.Results;

namespace BlazorDemo.Client.Shared.Types;

public class ApiResult<T> : Result<T, ProblemDetails>
{
    public static ApiResult<T> Success(T data)
    {
        return new ApiResult<T>(data, null);
    }

    public static ApiResult<T> Failure(ProblemDetails error)
    {
        return new ApiResult<T>(default, error);
    }

    private ApiResult(T? data, ProblemDetails? error) : base(data, error)
    {
    }
}
