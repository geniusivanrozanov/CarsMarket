using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Advertisement.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        return services
            .AddMediator()
            .AddTimeProvider();
    }

    private static IServiceCollection AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        return services;
    }

    private static IServiceCollection AddTimeProvider(this IServiceCollection services)
    {
        services.AddSingleton(TimeProvider.System);

        return services;
    }
}
