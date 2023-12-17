using CarsCatalog.Application.DTOs;
using MediatR;

namespace CarsCatalog.Application.Features.Queries;

public class GetModelByIdQuery : IRequest<GetModelDto>
{
    private readonly Guid _modelId;

    public GetModelByIdQuery(Guid modelId)
    {
        _modelId = modelId;
    }

    public Guid ModelId => _modelId;
}
