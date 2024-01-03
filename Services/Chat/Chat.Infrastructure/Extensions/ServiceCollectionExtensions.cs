using Chat.Application.Interfaces.Repositories;
using Chat.Infrastructure.Data.Contexts;
using Chat.Infrastructure.Data.Repositories;
using Chat.Infrastructure.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Chat.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services,
        IConfiguration configuration)
    {
        return services
            .AddMongoClient(configuration)
            .AddDbContexts()
            .AddRepositories()
            .ConfigureOptions(configuration);
    }
    
    private static IServiceCollection AddMongoClient(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MongoDB");
        ArgumentException.ThrowIfNullOrEmpty(connectionString);

        services.AddSingleton(new MongoClient(connectionString));

        return services;
    }
    
    private static IServiceCollection AddDbContexts(this IServiceCollection services)
    {
        services.AddScoped<ChatContext>();

        return services;
    }
    
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IChatRepository, ChatRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();

        return services;
    }
    
    private static IServiceCollection ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
    {
        var databaseName = configuration["MongoDatabaseName"];
        ArgumentException.ThrowIfNullOrEmpty(databaseName);

        services.Configure<DatabaseOptions>(options => options.DatabaseName = databaseName);

        return services;
    }
}
