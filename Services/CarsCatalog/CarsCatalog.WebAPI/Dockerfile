﻿FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 7001
EXPOSE 8001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Services/CarsCatalog/CarsCatalog.WebAPI/CarsCatalog.WebAPI.csproj", "Services/CarsCatalog/CarsCatalog.WebAPI/"]
RUN dotnet restore "Services/CarsCatalog/CarsCatalog.WebAPI/CarsCatalog.WebAPI.csproj"
COPY . .
WORKDIR "/src/Services/CarsCatalog/CarsCatalog.WebAPI"
RUN dotnet build "CarsCatalog.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarsCatalog.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarsCatalog.WebAPI.dll"]
