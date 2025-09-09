using HotChocolate.Types;
using System.Threading;
using System.Threading.Tasks;
using Todos.Api.Gql;
using Todos.Services.Models;
using Todos.Services.Interfaces;
using System;

namespace Todos.Api.Features.Todos;

[ExtendObjectType(Constants.Mutation)]
public class TodoMutations : IMutationExtension
{
    private readonly ITodoService _todoService;

    public TodoMutations(ITodoService todoService)
    {
        _todoService = todoService;
    }

    /// <summary>
    /// Creates a new todo (ID will be auto-generated)
    /// </summary>
    public async Task<TodoModel> CreateTodo(CreateTodoInput input, CancellationToken cancel = default)
    {
        return await _todoService.CreateTodoAsync(input);
    }

    /// <summary>
    /// Updates an existing todo
    /// </summary>
    public async Task<TodoModel> UpdateTodo(Guid id, UpdateTodoInput input, CancellationToken cancel = default)
    {
        var updatedTodo = await _todoService.UpdateTodoAsync(id, input);
        
        if (updatedTodo == null)
        {
            throw new NotFoundException($"Todo with ID {id} was not found.");
        }

        return updatedTodo;
    }

    /// <summary>
    /// Deletes a todo by ID
    /// </summary>
    public async Task<bool> DeleteTodo(Guid id, CancellationToken cancel = default)
    {
        var deleted = await _todoService.DeleteTodoAsync(id);
        
        if (!deleted)
        {
            throw new NotFoundException($"Todo with ID {id} was not found.");
        }

        return true;
    }
}