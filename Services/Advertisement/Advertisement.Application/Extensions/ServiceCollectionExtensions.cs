﻿using System.Reflection;
using FluentValidation;
using Grpc.Net.Client;
using Identity.gRPC.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf.Grpc.Client;
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
            .AddTimeProvider();
    }

    private static IServiceCollection AddGrpcClients(this IServiceCollection services, IConfiguration configuration)
    {
        var identityUri = configuration["IdentityConfiguration:Uri"];
        ArgumentException.ThrowIfNullOrEmpty(identityUri);

        services.AddCodeFirstGrpcClient<IIdentityService>(options =>
        {
            options.Address = new Uri(identityUri);
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
