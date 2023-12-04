using CarsCatalog.Application.DTOs;
using MediatR;

namespace CarsCatalog.Application.Features.Queries;

public class GetBrandByIdQuery : IRequest<GetBrandDto>
{
    private readonly Guid _brandId;

    public GetBrandByIdQuery(Guid brandId)
    {
        _brandId = brandId;
    }

    public Guid BrandId => _brandId;
}
