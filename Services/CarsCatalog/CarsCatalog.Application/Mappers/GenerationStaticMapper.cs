using CarsCatalog.Application.DTOs;
using CarsCatalog.Domain.Entities;
using CarsCatalog.gRPC.Contracts.Replies;
using CarsCatalog.Messages.Contracts;
using Riok.Mapperly.Abstractions;

namespace CarsCatalog.Application.Mappers;

[Mapper]
public static partial class GenerationStaticMapper
{
    public static partial GetGenerationDto ToGetGenerationDto(this GenerationEntity generationEntity);
    [MapProperty($"{nameof(GenerationEntity.Name)}", $"{nameof(GetModificationNamesReply.GenerationName)}")]
    [MapProperty($"{nameof(GenerationEntity.Model)}.{nameof(GenerationEntity.Model.Name)}", $"{nameof(GetModificationNamesReply.ModelName)}")]
    [MapProperty($"{nameof(GenerationEntity.Model)}.{nameof(GenerationEntity.Model.Brand)}.{nameof(GenerationEntity.Model.Brand.Name)}", $"{nameof(GetModificationNamesReply.BrandName)}")]
    public static partial GetModificationNamesReply ToGetModificationNamesReplyDto(this GenerationEntity generationEntity);
    [MapProperty(nameof(GenerationEntity.Id), nameof(GenerationUpdatedMessage.GenerationId))]
    [MapProperty(nameof(GenerationEntity.Name), nameof(GenerationUpdatedMessage.UpdatedGenerationName))]
    public static partial GenerationUpdatedMessage ToGenerationUpdatedMessage(this GenerationEntity generationEntity);

    public static partial GenerationEntity ToGenerationEntity(this CreateGenerationDto createGenerationDto);
    public static partial GenerationEntity ToGenerationEntity(this UpdateGenerationDto updateGenerationDto);

    public static partial void ToGenerationEntity(this UpdateGenerationDto updateGenerationDto,
        GenerationEntity generationEntity);

    public static partial IQueryable<GetGenerationDto> ProjectToGetGenerationDto(
        this IQueryable<GenerationEntity> queryable);
    
    public static partial IQueryable<GetModificationNamesReply> ProjectToGetModificationNamesReply(
        this IQueryable<GenerationEntity> queryable);
    
    public static IQueryable<TResult> ProjectTo<TResult>(this IQueryable<GenerationEntity> queryable)
    {
        if (queryable is IQueryable<TResult> query) return query;

        return queryable.MapTo<IQueryable<TResult>>();
    }

    private static partial TResult MapTo<TResult>(this object source);
}
