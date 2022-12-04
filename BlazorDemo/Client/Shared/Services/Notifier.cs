using MudBlazor;

namespace BlazorDemo.Client.Shared.Services;

public interface INotifier
{
    void ShowError(string message);
    void ShowSuccess(string message);
}

public class Notifier : INotifier
{
    private readonly ISnackbar _snackbar;

    public Notifier(ISnackbar snackbar)
    {
        _snackbar = snackbar;
    }

    public void ShowError(string message)
    {
        _snackbar.Add(message, Severity.Error);
    }

    public void ShowSuccess(string message)
    {
        _snackbar.Add(message, Severity.Success);
    }
}
