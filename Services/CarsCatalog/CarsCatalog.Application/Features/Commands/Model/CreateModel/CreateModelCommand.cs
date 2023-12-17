using CarsCatalog.Application.DTOs;
using MediatR;

namespace CarsCatalog.Application.Features.Commands;

public class CreateModelCommand : IRequest<GetModelDto>
{
    public CreateModelCommand(CreateModelDto createModelDto)
    {
        CreateModelDto = createModelDto;
    }

    public CreateModelDto CreateModelDto { get; }
}
