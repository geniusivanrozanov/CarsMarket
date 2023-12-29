using Advertisement.gRPC.Contracts;
using Chat.Application.Features.Services;
using Chat.Application.Interfaces.Services;
using Identity.gRPC.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf.Grpc.ClientFactory;

namespace Chat.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddServices()
            .AddGrpcClients(configuration)
            .AddTimeProvider();
    }
    
    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IChatService, ChatService>();
        services.AddScoped<IMessageService, MessageService>();

        return services;
    }
    
    private static IServiceCollection AddGrpcClients(this IServiceCollection services, IConfiguration configuration)
    {
        var identityUri = configuration["IdentityConfiguration:Uri"];
        var advertisementUri = configuration["AdvertisementConfiguration:Uri"];
        
        ArgumentException.ThrowIfNullOrEmpty(identityUri);
        ArgumentException.ThrowIfNullOrEmpty(advertisementUri);
        
        services.AddCodeFirstGrpcClient<IIdentityService>(options =>
        {
            options.Address = new Uri(identityUri);
        });
        
        services.AddCodeFirstGrpcClient<IAdvertisementService>(options =>
        {
            options.Address = new Uri(advertisementUri);
        });

        return services;
    }
    
    private static IServiceCollection AddTimeProvider(this IServiceCollection services)
    {
        services.AddSingleton(TimeProvider.System);

        return services;
    }
}
