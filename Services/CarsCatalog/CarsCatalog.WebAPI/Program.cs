using CarsCatalog.Application.Extensions;
using CarsCatalog.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructureLayer(configuration);

var app = builder.Build();

await app.Services.ApplyInfrastructureLayerAsync();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();