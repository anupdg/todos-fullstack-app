namespace Todos.Services.Models;

public class UpdateTodoInput
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public bool IsCompleted { get; set; }
}
