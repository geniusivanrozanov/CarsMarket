using CarsCatalog.Application.Exceptions;
using CarsCatalog.Application.Interfaces.Repositories;
using CarsCatalog.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarsCatalog.Application.Features.Commands;

public class DeleteModelCommandHandler(
    IRepositoryUnitOfWork repositoryUnitOfWork,
    ILogger<DeleteModelCommandHandler> logger) :
    IRequestHandler<DeleteModelCommand>
{
    private readonly IModelRepository _modelRepository = repositoryUnitOfWork.Models;

    public async Task Handle(DeleteModelCommand request, CancellationToken cancellationToken)
    {
        var entity = await _modelRepository.GetModelByIdAsync<ModelEntity>(request.ModelId, cancellationToken);

        if (entity is null)
        {
            logger.LogInformation("Model with id {Id} not exists", request.ModelId);
            throw new NotExistsException($"Model with id '{request.ModelId}' not exists.");
        }

        _modelRepository.DeleteModel(entity);
        await repositoryUnitOfWork.SaveAsync(cancellationToken);
    }
}
