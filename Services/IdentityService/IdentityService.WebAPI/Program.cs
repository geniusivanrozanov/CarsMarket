using Common.Logging;
using IdentityService.Application.Extensions;
using IdentityService.Infrastructure.Extensions;
using IdentityService.WebAPI.Extensions;
using IdentityService.WebAPI.Middlewares;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Host.UseSerilog(SeriLogger.Configure);

builder.Services
    .AddInfrastructureLayer(configuration)
    .AddApplicationLayer(configuration)
    .AddApiLayer();

var app = builder.Build();

await app.Services.ApplyInfrastructureLayerAsync();
await app.Services.ApplyApplicationLayerAsync();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.MapControllers();

app.Run();
