using MediatR;

namespace CarsCatalog.Application.Features.Commands;

public class DeleteGenerationCommand(Guid generationId) : IRequest
{
    public Guid GenerationId => generationId;
}
