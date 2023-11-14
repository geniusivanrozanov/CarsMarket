using CarsCatalog.Application.DTOs;
using MediatR;

namespace CarsCatalog.Application.Features.Commands;

public class CreateModelCommand(CreateModelDto createModelDto) : IRequest<GetModelDto>
{
    public CreateModelDto CreateModelDto { get; } = createModelDto;
}
