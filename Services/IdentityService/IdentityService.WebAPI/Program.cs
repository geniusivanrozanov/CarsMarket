using Common.Logging;
using IdentityService.Application.Extensions;
using IdentityService.Infrastructure.Extensions;
using IdentityService.WebAPI.Extensions;
using IdentityService.WebAPI.Middlewares;
using Serilog;

Console.WriteLine("-- start --");

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Host.UseSerilog(SeriLogger.Configure);

builder.Services
    .AddInfrastructureLayer(configuration)
    .AddApplicationLayer(configuration)
    .AddApiLayer();

var app = builder.Build();

await app.Services.ApplyInfrastructureLayer();
await app.Services.ApplyApplicationLayer();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.MapControllers();

app.Run();
