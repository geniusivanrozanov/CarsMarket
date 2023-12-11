using CarsCatalog.Application.DTOs;
using CarsCatalog.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace CarsCatalog.Application.Mappers;

[Mapper]
public static partial class ModelStaticMapper
{
    public static partial GetModelDto ToGetModelDto(this ModelEntity modelEntity);
    public static partial ModelEntity ToModelEntity(this CreateModelDto createModelDto);
    public static partial ModelEntity ToModelEntity(this UpdateModelDto updateModelDto);
    public static partial void ToModelEntity(this UpdateModelDto updateModelDto, ModelEntity modelEntity);
    public static partial IQueryable<GetModelDto> ProjectToGetModelDto(this IQueryable<ModelEntity> queryable);
    
    public static IQueryable<TResult> ProjectTo<TResult>(this IQueryable<ModelEntity> queryable)
    {
        if (queryable is IQueryable<TResult> query) return query;

        return queryable.MapTo<IQueryable<TResult>>();
    }

    private static partial TResult MapTo<TResult>(this object source);
}
