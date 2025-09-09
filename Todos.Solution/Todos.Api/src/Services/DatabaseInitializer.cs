using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Todos.Data;

namespace Todos.Api.Services;

public class DatabaseInitializer : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<DatabaseInitializer> _logger;

    public DatabaseInitializer(IServiceProvider serviceProvider, ILogger<DatabaseInitializer> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<TodosDbContext>();

        try
        {
            _logger.LogInformation("Starting database migration...");
            
            // Apply any pending migrations
            await context.Database.MigrateAsync(cancellationToken);
            
            _logger.LogInformation("Database migration completed successfully.");

            // Optional: Seed initial data if needed
            await SeedDataAsync(context, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while migrating the database.");
            throw;
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private async Task SeedDataAsync(TodosDbContext context, CancellationToken cancellationToken)
    {
        // Check if there's any data already
        if (await context.Todos.AnyAsync(cancellationToken))
        {
            _logger.LogInformation("Database already contains data. Skipping seed.");
            return;
        }

        // Add some initial data if the database is empty
        _logger.LogInformation("Seeding initial data...");

        var initialTodos = new[]
        {
            new Todo
            {
                Title = "Welcome to Todos API",
                Description = "This is your first todo item created during database initialization.",
                Status = "Pending",
                IsCompleted = false
            },
            new Todo
            {
                Title = "Explore GraphQL endpoints",
                Description = "Try out the GraphQL queries and mutations.",
                Status = "Pending",
                IsCompleted = false
            }
        };

        context.Todos.AddRange(initialTodos);
        await context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Initial data seeded successfully.");
    }
}