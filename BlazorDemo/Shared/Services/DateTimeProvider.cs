namespace BlazorDemo.Shared.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now() => DateTime.UtcNow;
}
