using CarsCatalog.Application.DTOs;
using MediatR;

namespace CarsCatalog.Application.Features.Commands;

public class UpdateGenerationCommand(Guid generationId, UpdateGenerationDto updateGenerationDto) : IRequest<GetGenerationDto>
{
    public Guid GenerationId => generationId;
    public UpdateGenerationDto UpdateGenerationDto => updateGenerationDto;
}
