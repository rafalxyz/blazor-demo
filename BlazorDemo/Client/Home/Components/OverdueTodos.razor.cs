using BlazorDemo.Client.Shared.State;
using BlazorDemo.Client.Todos.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorDemo.Client.Home.Components;

public partial class OverdueTodos
{
    [Inject] private OverdueTodosState OverdueTodosState { get; set; } = null!;
    [Inject] private ITodoClient TodoClient { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        var result = await TodoClient.GetOverdueCount();
        result.OnSuccess(count => OverdueTodosState.SetCount(count));
        OverdueTodosState.OnChange += StateHasChanged;
    }
}
