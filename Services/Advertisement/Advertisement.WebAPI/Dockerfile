﻿FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8002
EXPOSE 7002

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Services/Advertisement/Advertisement.WebAPI/Advertisement.WebAPI.csproj", "Services/Advertisement/Advertisement.WebAPI/"]
RUN dotnet restore "Services/Advertisement/Advertisement.WebAPI/Advertisement.WebAPI.csproj"
COPY . .
WORKDIR "/src/Services/Advertisement/Advertisement.WebAPI"
RUN dotnet build "Advertisement.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Advertisement.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Advertisement.WebAPI.dll"]
