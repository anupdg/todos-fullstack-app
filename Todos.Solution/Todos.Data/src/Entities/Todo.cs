using System;
using System.ComponentModel.DataAnnotations;

namespace Todos.Data;

public class Todo
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public string Status { get; set; }
}