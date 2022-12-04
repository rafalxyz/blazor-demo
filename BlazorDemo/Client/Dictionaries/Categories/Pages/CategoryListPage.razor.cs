using BlazorDemo.Client.Dictionaries.Categories.Services;
using BlazorDemo.Shared.Dictionaries.Categories;
using Microsoft.AspNetCore.Components;

namespace BlazorDemo.Client.Dictionaries.Categories.Pages;

public partial class CategoryListPage
{
    [Inject] private ICategoryClient Client { get; set; } = null!;

    private List<CategoryDto> categories = null!;

    public CategoryListPage()
    {
    }

    protected override async Task OnInitializedAsync()
    {
        await LoadCategories();
    }

    private async Task LoadCategories()
    {
        var response = await Client.GetAll();
        response.OnSuccess(data => categories = data);
    }
}
