using HotChocolate.Types;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Todos.Api.Gql;
using Todos.Services.Models;
using Todos.Services.Interfaces;
using System;

namespace Todos.Api.Features.Todos;

[ExtendObjectType(Constants.Query)]
public class TodoQueries : IQueryExtension
{
    private readonly ITodoService _todoService;

    public TodoQueries(ITodoService todoService)
    {
        _todoService = todoService;
    }   

    /// <summary>
    /// Gets all todos
    /// </summary>
    public async Task<IEnumerable<TodoModel>> GetTodos(CancellationToken cancel = default)
    {
        return await _todoService.GetAllTodosAsync();
    }

    /// <summary>
    /// Gets a specific todo by ID
    /// </summary>
    public async Task<TodoModel> GetTodo(Guid id, CancellationToken cancel = default)
    {
        var todo = await _todoService.GetTodoByIdAsync(id);
        
        if (todo == null)
        {
            throw new NotFoundException($"Todo with ID {id} was not found.");
        }

        return todo;
    }
}
