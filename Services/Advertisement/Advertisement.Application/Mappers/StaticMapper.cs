using Advertisement.Application.DTOs.Ad;
using Advertisement.Application.DTOs.Price;
using Advertisement.Domain.Entities;
using Advertisement.Domain.ValueObjects;
using Riok.Mapperly.Abstractions;

namespace Advertisement.Application.Mappers;

[Mapper]
public static partial class StaticMapper
{
    [MapProperty(nameof(AdEntity.BrandName), nameof(GetAdDto.Brand))]
    [MapProperty(nameof(AdEntity.ModelName), nameof(GetAdDto.Model))]
    [MapProperty(nameof(AdEntity.GenerationName), nameof(GetAdDto.Generation))]
    [MapProperty($"{nameof(AdEntity.CurrentPrice)}.{nameof(AdEntity.CurrentPrice.Currency)}",
        nameof(GetAdDto.Currency))]
    [MapProperty($"{nameof(AdEntity.CurrentPrice)}.{nameof(AdEntity.CurrentPrice.Value)}", nameof(GetAdDto.Price))]
    public static partial GetAdDto ToGetAdDto(this AdEntity adEntity);

    public static partial IEnumerable<GetAdDto> ToGetAdDto(this IEnumerable<AdEntity> adEntities);

    public static partial AdEntity ToAdEntity(this CreateAdDto createAdDto);
    public static partial AdEntity ToAdEntity(this UpdateAdDto updateAdDto);
    public static partial void ToAdEntity(this UpdateAdDto updateAdDto, AdEntity adEntity);

    public static partial GetPriceDto ToGetPriceDto(this Price price);
    public static partial IEnumerable<GetPriceDto> ToGetPriceDto(this IEnumerable<Price> price);

    [MapProperty(nameof(CreateAdDto.Price), nameof(Price.Value))]
    public static partial Price ToPrice(this CreateAdDto createAdDto);

    [MapProperty(nameof(UpdateAdDto.Price), nameof(Price.Value))]
    public static partial Price ToPrice(this UpdateAdDto updateAdDto);
}
