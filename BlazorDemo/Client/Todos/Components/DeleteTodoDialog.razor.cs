using BlazorDemo.Client.Todos.Services;
using BlazorDemo.Shared.Todos;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BlazorDemo.Client.Todos.Components;

public partial class DeleteTodoDialog
{
    [CascadingParameter] private MudDialogInstance MudDialog { get; set; } = null!;

    [Parameter] public TodoDto Todo { get; set; } = null!;

    [Inject] private ITodoClient TodoClient { get; set; } = null!;

    private async Task Submit()
    {
        var result = await TodoClient.Delete(Todo.Id);

        result.OnSuccess(success =>
        {
            MudDialog.Close(DialogResult.Ok(success));
        });
    }

    private void Cancel() => MudDialog.Cancel();
}
