using MediatR;

namespace CarsCatalog.Application.Features.Commands;

public class DeleteModelCommand : IRequest
{
    private readonly Guid _modelId;

    public DeleteModelCommand(Guid modelId)
    {
        _modelId = modelId;
    }

    public Guid ModelId => _modelId;
}
