using CarsCatalog.Application.DTOs;
using CarsCatalog.Domain.Entities;
using CarsCatalog.Messages.Contracts;
using Riok.Mapperly.Abstractions;

namespace CarsCatalog.Application.Mappers;

[Mapper]
public static partial class BrandStaticMapper
{
    public static partial GetBrandDto ToGetBrandDto(this BrandEntity brandEntity);
    [MapProperty(nameof(BrandEntity.Id), nameof(BrandUpdatedMessage.BrandId))]
    [MapProperty(nameof(BrandEntity.Name), nameof(BrandUpdatedMessage.UpdatedBrandName))]
    public static partial BrandUpdatedMessage ToBrandUpdatedMessage(this BrandEntity brandEntity);
    public static partial BrandEntity ToBrandEntity(this CreateBrandDto createBrandDto);
    public static partial BrandEntity ToBrandEntity(this UpdateBrandDto updateBrandDto);
    public static partial void ToBrandEntity(this UpdateBrandDto updateBrandDto, BrandEntity brandEntity);
    public static partial IQueryable<GetBrandDto> ProjectToGetBrandDto(this IQueryable<BrandEntity> queryable);

    public static IQueryable<TResult> ProjectTo<TResult>(this IQueryable<BrandEntity> queryable)
    {
        if (queryable is IQueryable<TResult> query) return query;

        return queryable.MapTo<IQueryable<TResult>>();
    }

    private static partial TResult MapTo<TResult>(this object source);
}
