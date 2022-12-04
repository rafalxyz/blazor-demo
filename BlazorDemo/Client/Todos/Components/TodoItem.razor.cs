using BlazorDemo.Client.Shared.State;
using BlazorDemo.Client.Todos.Services;
using BlazorDemo.Shared.Services;
using BlazorDemo.Shared.Todos;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BlazorDemo.Client.Todos.Components;

public partial class TodoItem
{
    [Parameter] public TodoDto Todo { get; set; } = null!;
    [Parameter] public EventCallback OnDeleted { get; set; }

    [Inject] private IDialogService DialogService { get; set; } = null!;
    [Inject] private ITodoClient TodoClient { get; set; } = null!;
    [Inject] private IDateTimeProvider DateTimeProvider { get; set; } = null!;
    [Inject] private OverdueTodosState OverdueTodosState { get; set; } = null!;

    public bool IsOverdue => !Todo.IsCompleted && Todo.DueDate < DateTimeProvider.Now();
    public string PriorityName => Dictionaries.Priorities.Single(x => x.Value == Todo.Priority).Text;

    private async Task DeleteTodo()
    {
        var parameters = new DialogParameters
        {
            ["todo"] = Todo
        };

        var dialog = DialogService.Show<DeleteTodoDialog>("Delete todo", parameters);
        var result = await dialog.Result;

        if (result.Cancelled)
        {
            return;
        }

        await OnDeleted.InvokeAsync();
    }

    private async Task CompleteTodo()
    {
        var wasOverdue = IsOverdue;

        await TodoClient.Complete(Todo.Id);
        Todo.IsCompleted = true;

        if (wasOverdue)
        {
            OverdueTodosState.Decrease();
        }
    }
}
