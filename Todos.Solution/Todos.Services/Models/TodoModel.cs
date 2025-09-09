namespace Todos.Services.Models;

public class TodoModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public string Status { get; set; }
}