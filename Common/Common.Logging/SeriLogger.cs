using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace Common.Logging;

public static class SeriLogger
{
    public static Action<HostBuilderContext, LoggerConfiguration> Configure =>
        (context, configuration) =>
        {
            var elasticUri = context.Configuration.GetValue<string>("ElasticConfiguration:Uri");
            ArgumentException.ThrowIfNullOrEmpty(elasticUri);
            
            var applicationName = context.HostingEnvironment.ApplicationName
                .ToLower()
                .Replace(".", "-");
            var environmentName = context.HostingEnvironment.EnvironmentName
                .ToLower()
                .Replace(".", "-");

            configuration
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .WriteTo.Debug()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUri))
                {
                    IndexFormat = $"{applicationName}-logs-{environmentName}-{DateTime.UtcNow:yyyy-MM}",
                    AutoRegisterTemplate = true
                })
                .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                .Enrich.WithProperty("Application", context.HostingEnvironment.ApplicationName)
                .ReadFrom.Configuration(context.Configuration);
        };
}
