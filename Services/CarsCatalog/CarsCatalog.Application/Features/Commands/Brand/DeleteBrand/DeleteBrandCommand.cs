using MediatR;

namespace CarsCatalog.Application.Features.Commands;

public class DeleteBrandCommand(Guid brandId) : IRequest
{
    public Guid BrandId => brandId;
}
