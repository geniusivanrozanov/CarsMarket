using IdentityService.Application.Extensions;
using IdentityService.Infrastructure.Extensions;
using IdentityService.WebAPI.Extensions;
using IdentityService.WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services
    .AddInfrastructureLayer(configuration)
    .AddApplicationLayer(configuration)
    .AddApiLayer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.MapControllers();

app.Run();