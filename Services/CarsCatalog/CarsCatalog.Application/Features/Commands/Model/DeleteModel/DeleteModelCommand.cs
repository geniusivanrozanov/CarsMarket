using MediatR;

namespace CarsCatalog.Application.Features.Commands;

public class DeleteModelCommand(Guid modelId) : IRequest
{
    public Guid ModelId => modelId;
}
