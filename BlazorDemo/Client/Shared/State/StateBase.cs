namespace BlazorDemo.Client.Shared.State;

public class StateBase
{
    public event Action? OnChange;
    protected void NotifyStateChanged() => OnChange?.Invoke();
}
