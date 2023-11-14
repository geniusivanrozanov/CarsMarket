using CarsCatalog.Application.DTOs;
using MediatR;

namespace CarsCatalog.Application.Features.Commands;

public class UpdateModelCommand(Guid modelId, UpdateModelDto updateModelDto) : IRequest<GetModelDto>
{
    public Guid ModelId => modelId;
    public UpdateModelDto UpdateModelDto => updateModelDto;
}
