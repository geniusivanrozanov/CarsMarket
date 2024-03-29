﻿using System.Text;
using System.Text.Json.Serialization;
using FavoriteFilters.Application.Interfaces.Services;
using FavoriteFilters.WebAPI.Middlewares;
using FavoriteFilters.WebAPI.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace FavoriteFilters.WebAPI.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiLayer(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddControllers()
            .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); })
            .Services
            .AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            })
            .AddAuthorization()
            .ConfigureAuthentication(configuration)
            .AddEndpointsApiExplorer()
            .AddCorsDefaultPolicy()
            .AddServices()
            .AddValidators()
            .AddSwagger();
    }
    
    private static IServiceCollection AddCorsDefaultPolicy(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();
            });
        });

        return services;
    }
    
    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUser, CurrentUser>();

        return services;
    }
    
    private static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();

        return services;
    }
    
    private static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme.",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        return services;
    }
    
    private static IServiceCollection ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var key = configuration["JwtOptions:Key"];
        var issuer = configuration["JwtOptions:Issuer"];
        var audience = configuration["JwtOptions:Audience"];
        
        ArgumentException.ThrowIfNullOrEmpty(key);
        ArgumentException.ThrowIfNullOrEmpty(issuer);
        ArgumentException.ThrowIfNullOrEmpty(audience);

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                    ClockSkew = TimeSpan.Zero
                };
            });

        return services;
    }
}
