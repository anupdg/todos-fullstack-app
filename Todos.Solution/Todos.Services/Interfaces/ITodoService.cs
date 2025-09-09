using Todos.Services.Models;

namespace Todos.Services.Interfaces;

public interface ITodoService
{
    Task<IEnumerable<TodoModel>> GetAllTodosAsync();
    Task<TodoModel?> GetTodoByIdAsync(Guid id);
    Task<TodoModel> CreateTodoAsync(CreateTodoInput input);
    Task<TodoModel?> UpdateTodoAsync(Guid id, UpdateTodoInput input);
    Task<bool> DeleteTodoAsync(Guid id);
    Task<bool> TodoExistsAsync(Guid id);
}