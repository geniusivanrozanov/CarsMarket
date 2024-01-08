using Chat.Application.Extensions;
using Chat.Infrastructure.Extensions;
using Chat.WebAPI.Extensions;
using Chat.WebAPI.Hubs;
using Chat.WebAPI.Middlewares;
using Common.Logging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(SeriLogger.Configure);

var configuration = builder.Configuration;

builder.Services
    .AddApplicationLayer(configuration)
    .AddInfrastructureLayer(configuration)
    .AddApiLayer(configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseRouting();

app.UseMiddleware<WebSocketsMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapHub<MessageHub>("messages");

app.MapControllers();

app.Run();
