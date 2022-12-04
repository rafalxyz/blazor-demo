namespace BlazorDemo.Client.Shared.State;

public class OverdueTodosState : StateBase
{
    public int? Count { get; private set; }

    public void SetCount(int count)
    {
        Count = count;
        NotifyStateChanged();
    }

    public void Decrease()
    {
        SetCount(Count!.Value - 1);
    }
}
