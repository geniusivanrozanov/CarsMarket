using MediatR;

namespace CarsCatalog.Application.Features.Commands;

public class DeleteGenerationCommand : IRequest
{
    private readonly Guid _generationId;

    public DeleteGenerationCommand(Guid generationId)
    {
        _generationId = generationId;
    }

    public Guid GenerationId => _generationId;
}
