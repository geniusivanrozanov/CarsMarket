﻿FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8000
EXPOSE 7000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Services/IdentityService/IdentityService.WebAPI/IdentityService.WebAPI.csproj", "Services/IdentityService/IdentityService.WebAPI/"]
RUN dotnet restore "Services/IdentityService/IdentityService.WebAPI/IdentityService.WebAPI.csproj"
COPY . .
WORKDIR "/src/Services/IdentityService/IdentityService.WebAPI"
RUN dotnet build "IdentityService.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IdentityService.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdentityService.WebAPI.dll"]
