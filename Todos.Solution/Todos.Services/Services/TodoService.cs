using Microsoft.EntityFrameworkCore;
using Todos.Data;
using Todos.Services.Interfaces;
using Todos.Services.Models;

namespace Todos.Services.Services;

public class TodoService : ITodoService
{
    private readonly TodosDbContext _context;

    public TodoService(TodosDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TodoModel>> GetAllTodosAsync()
    {
        var todos = await _context.Todos.ToListAsync();
        return todos.Select(MapToModel);
    }

    public async Task<TodoModel?> GetTodoByIdAsync(Guid id)
    {
        var todo = await _context.Todos.FindAsync(id);
        return todo == null ? null : MapToModel(todo);
    }

    public async Task<TodoModel> CreateTodoAsync(CreateTodoInput input)
    {
        var todo = new Todo
        {
            Id = Guid.NewGuid(),
            Title = input.Title,
            Description = input.Description,
            Status = input.Status,
            IsCompleted = input.IsCompleted
        };

        _context.Todos.Add(todo);
        await _context.SaveChangesAsync();
        
        return MapToModel(todo);
    }

    public async Task<TodoModel?> UpdateTodoAsync(Guid id, UpdateTodoInput input)
    {
        var todo = await _context.Todos.FindAsync(id);
        if (todo == null) 
            return null;

        todo.Title = input.Title;
        todo.Description = input.Description;
        todo.Status = input.Status;
        todo.IsCompleted = input.IsCompleted;

        _context.Entry(todo).State = EntityState.Modified;
        
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await TodoExistsAsync(id))
            {
                return null;
            }
            throw;
        }

        return MapToModel(todo);
    }

    public async Task<bool> DeleteTodoAsync(Guid id)
    {
        var todo = await _context.Todos.FindAsync(id);
        if (todo == null)
            return false;

        _context.Todos.Remove(todo);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> TodoExistsAsync(Guid id)
    {
        return await _context.Todos.AnyAsync(e => e.Id == id);
    }

    private static TodoModel MapToModel(Todo todo)
    {
        return new TodoModel
        {
            Id = todo.Id,
            Title = todo.Title,
            Description = todo.Description,
            Status = todo.Status,
            IsCompleted = todo.IsCompleted
        };
    }
}