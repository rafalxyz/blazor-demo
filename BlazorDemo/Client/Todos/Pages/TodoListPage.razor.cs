using BlazorDemo.Client.Shared.Services;
using BlazorDemo.Client.Todos.Components;
using BlazorDemo.Client.Todos.Services;
using BlazorDemo.Shared;
using BlazorDemo.Shared.Todos;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BlazorDemo.Client.Todos.Pages;

public partial class TodoListPage
{
    [Inject] private ITodoClient TodoClient { get; set; } = null!;
    [Inject] private INotifier Notifier { get; set; } = null!;
    [Inject] private IDialogService DialogService { get; set; } = null!;

    private PagedResponse<TodoDto> todos = null!;

    protected override async Task OnInitializedAsync()
    {
        await LoadTodos();
    }

    private async Task LoadTodos(int pageNumber = 1)
    {
        var response = await TodoClient.Search(pageNumber, pageSize: 5);
        response.OnSuccess(data => todos = data);
    }

    private async Task AddTodo()
    {
        var dialog = DialogService.Show<CreateTodoDialog>();
        var result = await dialog.Result;

        if (result.Cancelled)
        {
            return;
        }

        Notifier.ShowSuccess("Todo added successfully!");
        await LoadTodos();
    }

    private async Task OnDeleted()
    {
        Notifier.ShowSuccess("Todo deleted successfully!");
        await LoadTodos();
    }

    private async Task OnPageChanged(int pageNumber)
    {
        await LoadTodos(pageNumber);
    }
}
