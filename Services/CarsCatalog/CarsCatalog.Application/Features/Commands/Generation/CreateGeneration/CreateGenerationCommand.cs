using CarsCatalog.Application.DTOs;
using MediatR;

namespace CarsCatalog.Application.Features.Commands;

public class CreateGenerationCommand(CreateGenerationDto createGenerationDto) : IRequest<GetGenerationDto>
{
    public CreateGenerationDto CreateGenerationDto { get; } = createGenerationDto;
}
