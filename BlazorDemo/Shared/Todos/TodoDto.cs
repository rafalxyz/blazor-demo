namespace BlazorDemo.Shared.Todos;

public class TodoDto
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public DateTime DueDate { get; set; }
    public bool IsCompleted { get; set; }
    public Priority Priority { get; set; }
    public string CategoryId { get; set; } = null!;
    public string CategoryName { get; set; } = null!;
}