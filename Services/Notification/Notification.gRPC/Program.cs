using Common.Logging;
using Notification.Application.Extensions;
using Notification.Application.Services;
using ProtoBuf.Grpc.Server;
using Serilog;

var builder = WebApplication.CreateSlimBuilder(args);

var configuration = builder.Configuration;

builder.Host.UseSerilog(SeriLogger.Configure);

builder.Services.AddApplicationLayer(configuration);
builder.Services.AddCodeFirstGrpc();

var app = builder.Build();

app.MapGrpcService<NotificationService>();

app.Run();
