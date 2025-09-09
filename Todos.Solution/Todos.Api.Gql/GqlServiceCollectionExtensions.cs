using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Todos.Api.Gql;

public static class GraphQLServiceCollectionExtensions
{
    public static IRequestExecutorBuilder AddQueryExtensions(this IRequestExecutorBuilder builder, Assembly assembly)
    {
        // Find all types implementing IQueryExtension
        var queryExtensions = assembly.GetTypes()
            .Where(t => typeof(IQueryExtension).IsAssignableFrom(t) && !t.IsInterface);

        foreach (var queryExtension in queryExtensions)
        {
            // Register the resolver class as scoped in DI container
            builder.Services.AddScoped(queryExtension);
            builder.AddTypeExtension(queryExtension);
        }

        return builder;
    }

    public static IRequestExecutorBuilder AddMutationExtensions(this IRequestExecutorBuilder builder, Assembly assembly)
    {
        // Find all types implementing IMutationExtension
        var mutationExtensions = assembly.GetTypes()
            .Where(t => typeof(IMutationExtension).IsAssignableFrom(t) && !t.IsInterface);

        foreach (var mutationExtension in mutationExtensions)
        {
            // Register the resolver class as scoped in DI container
            builder.Services.AddScoped(mutationExtension);
            builder.AddTypeExtension(mutationExtension);
        }

        return builder;
    }

    public static IRequestExecutorBuilder AddServerAndQueryMutationTypes(this IServiceCollection services, Assembly assembly)
    {
        return services
            .AddGraphQLServer()
            .ModifyRequestOptions(opt =>
            {
                opt.IncludeExceptionDetails = true;
            })
            .AddAuthorization()
            .AddQueryType(d => d.Name(Constants.Query))
            .AddQueryExtensions(assembly)
            .AddMutationType(d => d.Name(Constants.Mutation))
            .AddMutationExtensions(assembly)
            .AddInMemorySubscriptions();
    }
}