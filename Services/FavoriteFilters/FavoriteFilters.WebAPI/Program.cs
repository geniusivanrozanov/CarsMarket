using Common.Logging;
using FavoriteFilters.Application.Extensions;
using FavoriteFilters.Infrastructure.Extensions;
using FavoriteFilters.WebAPI.Extensions;
using FavoriteFilters.WebAPI.Middlewares;
using Hangfire;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(SeriLogger.Configure);

var configuration = builder.Configuration;

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
    app.UseHangfireDashboard();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
