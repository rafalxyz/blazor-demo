using BlazorDemo.Server.Extensions;
using BlazorDemo.Shared;
using BlazorDemo.Shared.Services;
using BlazorDemo.Shared.Todos;
using Microsoft.AspNetCore.Mvc;

namespace BlazorDemo.Server.Controllers;

[ApiController]
[Route("api/todos")]
public class TodoController : ControllerBase
{
    private static readonly List<TodoDto> Todos = new()
    {
        new TodoDto
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Learn Blazor",
            DueDate = DateTime.Now.Date.AddDays(7),
            IsCompleted = true,
            Priority = Priority.High,
            CategoryId = CategoryController.Categories[0].Id,
            CategoryName = CategoryController.Categories[0].Name,
        },
        new TodoDto
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Learn Cosmos",
            DueDate = DateTime.Now.Date.AddDays(-1),
            Priority = Priority.Low,
            CategoryId = CategoryController.Categories[5].Id,
            CategoryName = CategoryController.Categories[5].Name,
        },
        new TodoDto
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Learn Azure Functions",
            DueDate = DateTime.Now.Date.AddDays(-1),
            Priority = Priority.Normal,
            CategoryId = CategoryController.Categories[2].Id,
            CategoryName = CategoryController.Categories[2].Name,
        },
        new TodoDto
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Learn Kubernetes",
            DueDate = DateTime.Now.Date.AddDays(14),
            Priority = Priority.Normal,
            CategoryId = CategoryController.Categories[3].Id,
            CategoryName = CategoryController.Categories[3].Name,
        },
        new TodoDto
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Learn Azure DevOps",
            DueDate = DateTime.Now.Date.AddDays(14),
            Priority = Priority.Normal,
            CategoryId = CategoryController.Categories[2].Id,
            CategoryName = CategoryController.Categories[2].Name,
        },
        new TodoDto
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Learn Angular",
            DueDate = DateTime.Now.Date.AddDays(14),
            Priority = Priority.Normal,
            CategoryId = CategoryController.Categories[1].Id,
            CategoryName = CategoryController.Categories[1].Name,
        },
        new TodoDto
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Learn React",
            DueDate = DateTime.Now.Date.AddDays(14),
            Priority = Priority.Normal,
            CategoryId = CategoryController.Categories[1].Id,
            CategoryName = CategoryController.Categories[1].Name,
        }
    };

    private readonly IDateTimeProvider _dateTimeProvider;

    public TodoController(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    [HttpGet("search")]
    public IActionResult Search(int pageNumber, int pageSize)
    {
        var response = Todos.ToPagedResponse(pageNumber, pageSize);
        return new JsonResult(response);
    }

    [HttpGet("overdueCount")]
    public IActionResult GetOverdueCount()
    {
        var count = Todos.Count(x => x.DueDate < _dateTimeProvider.Now().Date && !x.IsCompleted);
        return new JsonResult(count);
    }

    [HttpPost("")]
    public IActionResult Create(TodoCreateDto dto)
    {
        var newTodo = new TodoDto
        {
            Id = Guid.NewGuid().ToString(),
            Name = dto.Name,
            DueDate = dto.DueDate!.Value,
            Priority = dto.Priority,
            CategoryId = dto.CategoryId,
            CategoryName = CategoryController.Categories.Single(x => x.Id == dto.CategoryId).Name
        };
        Todos.Add(newTodo);
        return new JsonResult(new IdResponse(newTodo.Id));
    }

    [HttpPost("{id}")]
    public IActionResult Complete(string id)
    {
        var todo = Todos.Single(x => x.Id == id);
        todo.IsCompleted = true;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
        Todos.RemoveAll(x => x.Id == id);
        return NoContent();
    }
}