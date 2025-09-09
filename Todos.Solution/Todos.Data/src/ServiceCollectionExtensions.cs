using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Todos.Data;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTodosData(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TodosDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
}   