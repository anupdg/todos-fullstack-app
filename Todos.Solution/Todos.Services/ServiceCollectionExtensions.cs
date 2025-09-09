using Microsoft.Extensions.DependencyInjection;
using Todos.Services.Interfaces;
using Todos.Services.Services;

namespace Todos.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTodoServices(this IServiceCollection services)
    {
        services.AddScoped<ITodoService, TodoService>();
        
        return services;
    }
}