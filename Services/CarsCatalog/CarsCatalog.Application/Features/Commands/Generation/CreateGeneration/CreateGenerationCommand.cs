using CarsCatalog.Application.DTOs;
using MediatR;

namespace CarsCatalog.Application.Features.Commands;

public class CreateGenerationCommand : IRequest<GetGenerationDto>
{
    public CreateGenerationCommand(CreateGenerationDto createGenerationDto)
    {
        CreateGenerationDto = createGenerationDto;
    }

    public CreateGenerationDto CreateGenerationDto { get; }
}
