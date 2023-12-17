using CarsCatalog.Application.DTOs;
using MediatR;

namespace CarsCatalog.Application.Features.Commands;

public class UpdateGenerationCommand : IRequest<GetGenerationDto>
{
    private readonly Guid _generationId;
    private readonly UpdateGenerationDto _updateGenerationDto;

    public UpdateGenerationCommand(Guid generationId, UpdateGenerationDto updateGenerationDto)
    {
        _generationId = generationId;
        _updateGenerationDto = updateGenerationDto;
    }

    public Guid GenerationId => _generationId;
    public UpdateGenerationDto UpdateGenerationDto => _updateGenerationDto;
}
