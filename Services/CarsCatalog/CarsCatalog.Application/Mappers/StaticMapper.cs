﻿using CarsCatalog.Application.DTOs;
using CarsCatalog.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace CarsCatalog.Application.Mappers;

[Mapper]
public static partial class StaticMapper
{
    public static partial GetBrandDto ToGetBrandDto(this BrandEntity brandEntity);
    public static partial BrandEntity ToBrandEntity(this CreateBrandDto createBrandDto);
    public static partial BrandEntity ToBrandEntity(this UpdateBrandDto updateBrandDto);
    public static partial void ToBrandEntity(this UpdateBrandDto updateBrandDto, BrandEntity brandEntity);

    public static partial IQueryable<GetBrandDto> ProjectToGetBrandDto(this IQueryable<BrandEntity> queryable);
    
    public static partial TResult MapTo<TResult>(this object source);

    public static IQueryable<TResult> ProjectTo<TResult>(this IQueryable<object> queryable)
    {
        if (queryable is IQueryable<TResult> query)
        {
            return query;
        }

        return queryable.MapTo<IQueryable<TResult>>();
    }
}
