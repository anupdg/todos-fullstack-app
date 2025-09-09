using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;
using Todos.Api.Gql;
using Todos.Api.Services;
using Todos.Data;
using Todos.Services;

namespace Todos.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container
        ConfigureServices(builder.Services, builder.Configuration);

        var app = builder.Build();

        // Configure the HTTP request pipeline
        Configure(app);

        app.Run();
    }

    private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        // Add CORS
        services.AddCors(options =>
        {
            options.AddPolicy("AllowLocalhost", policy =>
            {
                var allowedOrigins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [];
                policy.WithOrigins(allowedOrigins)
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials();
            });
        });

        // Add DbContext
        services.AddTodosData(configuration);

        services.AddHostedService<DatabaseInitializer>();

        // Add Todo services
        services.AddTodoServices();

        // Add GraphQL with extensions
        services.AddServerAndQueryMutationTypes(Assembly.GetExecutingAssembly());
    }

    private static void Configure(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        // Enable CORS
        app.UseCors("AllowLocalhost");

        app.UseRouting();

        // Add health check endpoint
        app.MapGet("/health", () => Microsoft.AspNetCore.Http.Results.Ok(new { status = "healthy", timestamp = DateTime.UtcNow }));

        app.MapGraphQL();
    }
}