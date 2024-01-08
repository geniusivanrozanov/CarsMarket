using CarsCatalog.Application.Extensions;
using CarsCatalog.Application.Features.Services;
using CarsCatalog.Infrastructure.Extensions;
using CarsCatalog.WebAPI.Extensions;
using CarsCatalog.WebAPI.Middlewares;
using Common.Logging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Host.UseSerilog(SeriLogger.Configure);

builder.Services
    .AddApplicationLayer(configuration)
    .AddInfrastructureLayer(configuration)
    .AddApiLayer(configuration);

var app = builder.Build();

await app.Services.ApplyInfrastructureLayerAsync();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.MapGrpcService<CarsCatalogService>();

app.MapControllers();

app.Run();
