FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/FavoriteFilters/FavoriteFilters.WebAPI/FavoriteFilters.WebAPI.csproj", "Services/FavoriteFilters/FavoriteFilters.WebAPI/"]
RUN dotnet restore "Services/FavoriteFilters/FavoriteFilters.WebAPI/FavoriteFilters.WebAPI.csproj"
COPY . .
WORKDIR "/src/Services/FavoriteFilters/FavoriteFilters.WebAPI"
RUN dotnet build "FavoriteFilters.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "FavoriteFilters.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FavoriteFilters.WebAPI.dll"]
