using CarsCatalog.Application.DTOs;
using CarsCatalog.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace CarsCatalog.Application.Mappers;

[Mapper]
public static partial class StaticMapper
{
    public static partial GetBrandDto ToGetBrandDto(this BrandEntity brandEntity);
    public static partial BrandEntity ToBrandEntity(this CreateBrandDto createBrandDto);
    public static partial BrandEntity ToBrandEntity(this UpdateBrandDto updateBrandDto);
}
