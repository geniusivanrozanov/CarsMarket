FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 7004

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Notification/Notification.gRPC/Notification.gRPC.csproj", "Services/Notification/Notification.gRPC/"]
RUN dotnet restore "Services/Notification/Notification.gRPC/Notification.gRPC.csproj"
COPY . .
WORKDIR "/src/Services/Notification/Notification.gRPC"
RUN dotnet build "Notification.gRPC.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Notification.gRPC.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Notification.gRPC.dll"]
