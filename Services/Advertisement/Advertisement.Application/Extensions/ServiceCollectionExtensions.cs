using System.Reflection;
using Advertisement.Application.Features.Consumers;
using CarsCatalog.gRPC.Contracts;
using FluentValidation;
using Identity.gRPC.Contracts;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf.Grpc.ClientFactory;

namespace Advertisement.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddMediator()
            .AddValidators()
            .AddGrpcClients(configuration)
            .ConfigureMassTransit(configuration)
            .AddTimeProvider();
    }

    private static IServiceCollection ConfigureMassTransit(this IServiceCollection services, IConfiguration configuration)
    {
        var host = configuration["RabbitMQConfiguration:Host"];
        var username = configuration["RabbitMQConfiguration:Username"];
        var password = configuration["RabbitMQConfiguration:Password"];
        
        ArgumentException.ThrowIfNullOrEmpty(host);
        ArgumentException.ThrowIfNullOrEmpty(username);
        ArgumentException.ThrowIfNullOrEmpty(password);
        
        
        services.AddMassTransit(configurator =>
        {
            configurator.AddConsumers(Assembly.GetExecutingAssembly());
            
            configurator.UsingRabbitMq((context, factoryConfigurator) =>
            {
                factoryConfigurator.Host(host, hostConfigurator =>
                {
                    hostConfigurator.Username(username);
                    hostConfigurator.Password(password);
                });
                
                factoryConfigurator.ConfigureEndpoints(context);
            });
        });

        return services;
    }
    
    private static IServiceCollection AddGrpcClients(this IServiceCollection services, IConfiguration configuration)
    {
        var identityUri = configuration["IdentityConfiguration:Uri"];
        var carsCatalogUri = configuration["CarsCatalogConfiguration:Uri"];
        
        ArgumentException.ThrowIfNullOrEmpty(identityUri);
        ArgumentException.ThrowIfNullOrEmpty(carsCatalogUri);

        services.AddCodeFirstGrpcClient<IIdentityService>(options =>
        {
            options.Address = new Uri(identityUri);
        });
        
        services.AddCodeFirstGrpcClient<ICarsCatalogService>(options =>
        {
            options.Address = new Uri(carsCatalogUri);
        });
        
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
}
