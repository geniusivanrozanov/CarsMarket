using FluentValidation.AspNetCore;
using IdentityService.Application.Extensions;
using IdentityService.Infrastructure.Extensions;
using IdentityService.WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ExceptionHandlerMiddleware>();

builder.Services.AddInfrastructureLayer(configuration);
builder.Services.AddApplicationLayer(configuration);

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