using Advertisement.Application.Extensions;
using Advertisement.Infrastructure.Extensions;
using Advertisement.WebAPI.Extensions;
using Advertisement.WebAPI.Middlewares;
using Common.Logging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(SeriLogger.Configure);

var configuration = builder.Configuration;

builder.Services
    .AddApplicationLayer()
    .AddInfrastructureLayer(configuration)
    .AddApiLayer(configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
