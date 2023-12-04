using CarsCatalog.Application.DTOs;
using MediatR;

namespace CarsCatalog.Application.Features.Commands;

public class UpdateBrandCommand : IRequest<GetBrandDto>
{
    private readonly Guid _brandId;
    private readonly UpdateBrandDto _updateBrandDto;

    public UpdateBrandCommand(Guid brandId, UpdateBrandDto updateBrandDto)
    {
        _brandId = brandId;
        _updateBrandDto = updateBrandDto;
    }

    public Guid BrandId => _brandId;
    public UpdateBrandDto UpdateBrandDto => _updateBrandDto;
}
