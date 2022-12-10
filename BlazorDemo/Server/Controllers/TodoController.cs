using BlazorDemo.Server.Services;
using BlazorDemo.Shared;
using BlazorDemo.Shared.Todos;
using Microsoft.AspNetCore.Mvc;

namespace BlazorDemo.Server.Controllers;

[ApiController]
[Route("api/todos")]
public class TodoController : ControllerBase
{
    private readonly ITodoService _todoService;

    public TodoController(ITodoService todoService)
    {
        _todoService = todoService;
    }

    [HttpGet("search")]
    public IActionResult Search(int pageNumber, int pageSize)
    {
        var response = _todoService.Search(pageNumber, pageSize);
        return new JsonResult(response);
    }

    [HttpGet("overdueCount")]
    public IActionResult GetOverdueCount()
    {
        var count = _todoService.GetOverdueCount();
        return new JsonResult(count);
    }

    [HttpPost("")]
    public IActionResult Create(TodoCreateDto dto)
    {
        var newTodo = _todoService.Create(dto);
        return new JsonResult(new IdResponse(newTodo.Id));
    }

    [HttpPost("{id}")]
    public IActionResult Complete(string id)
    {
        _todoService.Complete(id);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
        _todoService.Delete(id);
        return NoContent();
    }

    // TODO: Just for testing
    [HttpDelete("")]
    public IActionResult DeleteAll()
    {
        _todoService.DeleteAll();
        return NoContent();
    }
}