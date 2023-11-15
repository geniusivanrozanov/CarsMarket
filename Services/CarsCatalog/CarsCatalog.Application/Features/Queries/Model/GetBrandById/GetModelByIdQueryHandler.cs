using CarsCatalog.Application.DTOs;
using CarsCatalog.Application.Exceptions;
using CarsCatalog.Application.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using MediatR;

namespace CarsCatalog.Application.Features.Queries;

public class GetModelByIdQueryHandler(
    IRepositoryUnitOfWork repositoryUnitOfWork,
    ILogger<GetModelByIdQueryHandler> logger) :
    IRequestHandler<GetModelByIdQuery, GetModelDto>
{
    private readonly IModelRepository _modelRepository = repositoryUnitOfWork.Models;

    public async Task<GetModelDto> Handle(GetModelByIdQuery request, CancellationToken cancellationToken)
    {
        var dto = await _modelRepository.GetModelByIdAsync<GetModelDto>(request.ModelId, cancellationToken);

        if (dto is null)
        {
            logger.LogInformation("Model with id {Id} not exists", request.ModelId);
            throw new NotExistsException($"Model with id '{request.ModelId}' not exists.");
        }

        return dto;
    }
}
