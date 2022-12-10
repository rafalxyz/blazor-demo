using BlazorDemo.Server.Controllers;
using BlazorDemo.Server.Extensions;
using BlazorDemo.Shared;
using BlazorDemo.Shared.Services;
using BlazorDemo.Shared.Todos;

namespace BlazorDemo.Server.Services;

public interface ITodoService
{
    void Complete(string id);
    TodoDto Create(TodoCreateDto dto);
    void Delete(string id);
    void DeleteAll();
    int GetOverdueCount();
    PagedResponse<TodoDto> Search(int pageNumber, int pageSize);
}

public class TodoService : ITodoService
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

    public TodoService(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public PagedResponse<TodoDto> Search(int pageNumber, int pageSize)
    {
        return Todos.ToPagedResponse(pageNumber, pageSize);
    }

    public int GetOverdueCount()
    {
        return Todos.Count(x => x.DueDate < _dateTimeProvider.Now().Date && !x.IsCompleted);
    }

    public TodoDto Create(TodoCreateDto dto)
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
        return newTodo;
    }

    public void Complete(string id)
    {
        var todo = Todos.Single(x => x.Id == id);
        todo.IsCompleted = true;
    }

    public void Delete(string id)
    {
        Todos.RemoveAll(x => x.Id == id);
    }

    public void DeleteAll()
    {
        Todos.RemoveAll(_ => true);
    }
}
