﻿using FluentValidation.AspNetCore;
using IdentityService.WebAPI.Middlewares;
using Microsoft.OpenApi.Models;

namespace IdentityService.WebAPI.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApiLayer(this IServiceCollection services)
    {
        return services
            .AddControllers().Services
            .AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            })
            .AddEndpointsApiExplorer()
            .AddValidators()
            .AddMiddlewares()
            .AddSwagger();
    }
    
    private static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        
        return services;
    }
    
    private static IServiceCollection AddMiddlewares(this IServiceCollection services)
    {
        services.AddSingleton<ExceptionHandlerMiddleware>();
        
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
                    new string[] {}
                }
            });
        });
        
        return services;
    }
}