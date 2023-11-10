using CarsCatalog.Application.DTOs;
using MediatR;

namespace CarsCatalog.Application.Features.Commands;

public class UpdateBrandCommand(Guid brandId, UpdateBrandDto updateBrandDto) : IRequest<GetBrandDto>
{
    public Guid BrandId => brandId;
    public UpdateBrandDto UpdateBrandDto => updateBrandDto;
}
