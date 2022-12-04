using BlazorDemo.Client.Dictionaries.Categories.Services;
using BlazorDemo.Client.Todos.Services;
using BlazorDemo.Shared.Dictionaries.Categories;
using BlazorDemo.Shared.Todos;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BlazorDemo.Client.Todos.Components;

public partial class CreateTodoDialog
{
    [CascadingParameter] private MudDialogInstance MudDialog { get; set; } = null!;

    [Inject] private ITodoClient TodoClient { get; set; } = null!;
    [Inject] private ICategoryClient CategoryClient { get; set; } = null!;
    [Inject] private TodoCreateDtoValidator Validator { get; set; } = null!;

    private List<CategoryDto> categories = new List<CategoryDto>();

    private TodoCreateDto model = new();
    private MudForm? form;

    protected override async Task OnInitializedAsync()
    {
        var result = await CategoryClient.GetAll();
        result.OnSuccess(data => categories = data);
    }


    private async Task Submit()
    {
        await form!.Validate();

        if (!form.IsValid)
        {
            return;
        }

        var result = await TodoClient.Create(model);

        result.OnSuccess(id =>
        {
            MudDialog.Close(DialogResult.Ok(id));
        });
    }

    private void Cancel() => MudDialog.Cancel();
}
