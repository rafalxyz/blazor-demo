namespace BlazorDemo.Shared.Results;

public class Result<TData, TError>
{
    public TData? Data { get; }
    public TError? Error { get; }
    public bool IsSuccess => Error is null;

    protected Result(TData? data, TError? error)
    {
        Data = data;
        Error = error;
    }

    public void OnSuccess(Action<TData> callback)
    {
        if (IsSuccess)
        {
            callback(Data!);
        }
    }

    public void OnError(Action<TError> callback)
    {
        if (!IsSuccess)
        {
            callback(Error!);
        }
    }

    public static implicit operator Result<TData, TError>(TData data)
    {
        return new Result<TData, TError>(data, default);
    }

    public static implicit operator Result<TData, TError>(TError error)
    {
        return new Result<TData, TError>(default, error);
    }
}
