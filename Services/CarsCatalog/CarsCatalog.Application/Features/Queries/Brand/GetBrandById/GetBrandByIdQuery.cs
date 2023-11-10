using CarsCatalog.Application.DTOs;
using MediatR;

namespace CarsCatalog.Application.Features.Queries;

public class GetBrandByIdQuery(Guid brandId) : IRequest<GetBrandDto>
{
    public Guid BrandId => brandId;
}
