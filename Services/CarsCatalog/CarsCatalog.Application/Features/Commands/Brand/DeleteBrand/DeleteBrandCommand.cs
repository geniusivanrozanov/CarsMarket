using MediatR;

namespace CarsCatalog.Application.Features.Commands;

public class DeleteBrandCommand : IRequest
{
    private readonly Guid _brandId;

    public DeleteBrandCommand(Guid brandId)
    {
        _brandId = brandId;
    }

    public Guid BrandId => _brandId;
}
