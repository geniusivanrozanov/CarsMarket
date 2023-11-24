using CarsCatalog.Application.Extensions;
using CarsCatalog.Infrastructure.Extensions;
using CarsCatalog.WebAPI.Extensions;
using CarsCatalog.WebAPI.Middlewares;
using Common.Logging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Host.UseSerilog(SeriLogger.Configure);

builder.Services
    .AddApplicationLayer()
    .AddInfrastructureLayer(configuration)
    .AddApiLayer();

var app = builder.Build();

await app.Services.ApplyInfrastructureLayerAsync();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.MapControllers();

app.Run();
