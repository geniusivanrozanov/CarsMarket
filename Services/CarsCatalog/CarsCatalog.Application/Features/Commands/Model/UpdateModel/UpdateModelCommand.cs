using CarsCatalog.Application.DTOs;
using MediatR;

namespace CarsCatalog.Application.Features.Commands;

public class UpdateModelCommand : IRequest<GetModelDto>
{
    private readonly Guid _modelId;
    private readonly UpdateModelDto _updateModelDto;

    public UpdateModelCommand(Guid modelId, UpdateModelDto updateModelDto)
    {
        _modelId = modelId;
        _updateModelDto = updateModelDto;
    }

    public Guid ModelId => _modelId;
    public UpdateModelDto UpdateModelDto => _updateModelDto;
}
