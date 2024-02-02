using System.Reflection;
using Advertisement.gRPC.Contracts;
using FavoriteFilters.Application.Features.Services;
using FavoriteFilters.Application.Interfaces.Services;
using FluentValidation;
using Hangfire;
using Hangfire.PostgreSql;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notification.gRPC.Contracts;
using ProtoBuf.Grpc.ClientFactory;

namespace FavoriteFilters.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddMediator()
            .AddGrpcClients(configuration)
            .AddHangfire(configuration)
            .AddValidators()
            .AddServices()
            .AddMapper()
            .AddTimeProvider();
        
        return services;
    }

    private static IServiceCollection AddMapper(this IServiceCollection services)
    {
        var config = new TypeAdapterConfig();
        config.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }

    
    private static IServiceCollection AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        return services;
    }
    
    private static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
    
    private static IServiceCollection AddTimeProvider(this IServiceCollection services)
    {
        services.AddSingleton(TimeProvider.System);

        return services;
    }
    
    private static IServiceCollection AddHangfire(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("FiltersPostgres");
        
        ArgumentException.ThrowIfNullOrEmpty(connectionString);
        
        services.AddHangfire(globalConfiguration =>
        {
            globalConfiguration.UsePostgreSqlStorage(options =>
            {
                options.UseNpgsqlConnection(connectionString);
            });
        });

        services.AddHangfireServer();

        return services;
    }
    
    private static IServiceCollection AddGrpcClients(this IServiceCollection services, IConfiguration configuration)
    {
        var advertisementUri = configuration["AdvertisementConfiguration:Uri"];
        var notificationUri = configuration["NotificationConfiguration:Uri"];
        
        ArgumentException.ThrowIfNullOrEmpty(advertisementUri);
        ArgumentException.ThrowIfNullOrEmpty(notificationUri);

        services.AddCodeFirstGrpcClient<IAdvertisementService>(options =>
        {
            options.Address = new Uri(advertisementUri);
        });
        
        services.AddCodeFirstGrpcClient<INotificationService>(options =>
        {
            options.Address = new Uri(notificationUri);
        });
        
        return services;
    }
    
    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IFiltersNotificationService, FiltersNotificationService>();
        
        return services;
    }
}
