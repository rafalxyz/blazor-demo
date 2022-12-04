using BlazorDemo.Shared.Dictionaries.Categories;
using Microsoft.AspNetCore.Mvc;

namespace BlazorDemo.Server.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoryController : ControllerBase
{
    public static readonly List<CategoryDto> Categories = new()
    {
        new CategoryDto { Id = Guid.NewGuid().ToString(), Name = ".NET" },
        new CategoryDto { Id = Guid.NewGuid().ToString(), Name = "Frontend" },
        new CategoryDto { Id = Guid.NewGuid().ToString(), Name = "Azure" },
        new CategoryDto { Id = Guid.NewGuid().ToString(), Name = "Kubernetes" },
        new CategoryDto { Id = Guid.NewGuid().ToString(), Name = "Architecture" },
        new CategoryDto { Id = Guid.NewGuid().ToString(), Name = "Databases" },

    };

    [HttpGet("")]
    public IActionResult Get()
    {
        return new JsonResult(Categories);
    }
}
