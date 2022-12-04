namespace BlazorDemo.Shared.Todos;

public class TodoCreateDto
{
    public string Name { get; set; } = null!;
    public DateTime? DueDate { get; set; }
    public Priority Priority { get; set; }
    public string CategoryId { get; set; } = null!;
}
