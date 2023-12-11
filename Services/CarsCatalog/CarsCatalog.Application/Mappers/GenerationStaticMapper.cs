using CarsCatalog.Application.DTOs;
using CarsCatalog.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace CarsCatalog.Application.Mappers;

[Mapper]
public static partial class GenerationStaticMapper
{
    public static partial GetGenerationDto ToGetGenerationDto(this GenerationEntity generationEntity);
    public static partial GenerationEntity ToGenerationEntity(this CreateGenerationDto createGenerationDto);
    public static partial GenerationEntity ToGenerationEntity(this UpdateGenerationDto updateGenerationDto);

    public static partial void ToGenerationEntity(this UpdateGenerationDto updateGenerationDto,
        GenerationEntity generationEntity);

    public static partial IQueryable<GetGenerationDto> ProjectToGetGenerationDto(
        this IQueryable<GenerationEntity> queryable);
    
    public static IQueryable<TResult> ProjectTo<TResult>(this IQueryable<GenerationEntity> queryable)
    {
        if (queryable is IQueryable<TResult> query) return query;

        return queryable.MapTo<IQueryable<TResult>>();
    }

    private static partial TResult MapTo<TResult>(this object source);
}
