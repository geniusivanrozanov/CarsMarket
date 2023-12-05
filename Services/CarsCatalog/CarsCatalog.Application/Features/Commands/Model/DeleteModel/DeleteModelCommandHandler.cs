using CarsCatalog.Application.Exceptions;
using CarsCatalog.Application.Interfaces.Repositories;
using CarsCatalog.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarsCatalog.Application.Features.Commands;

public class DeleteModelCommandHandler :
    IRequestHandler<DeleteModelCommand>
{
    private readonly IModelRepository _modelRepository;
    private readonly IRepositoryUnitOfWork _repositoryUnitOfWork;
    private readonly ILogger<DeleteModelCommandHandler> _logger;

    public DeleteModelCommandHandler(IRepositoryUnitOfWork repositoryUnitOfWork,
        ILogger<DeleteModelCommandHandler> logger)
    {
        _repositoryUnitOfWork = repositoryUnitOfWork;
        _logger = logger;
        _modelRepository = repositoryUnitOfWork.Models;
    }

    public async Task Handle(DeleteModelCommand request, CancellationToken cancellationToken)
    {
        var entity = await _modelRepository.GetModelByIdAsync<ModelEntity>(request.ModelId, cancellationToken);

        if (entity is null)
        {
            _logger.LogInformation("Model with id {Id} not exists", request.ModelId);
            throw new NotExistsException($"Model with id '{request.ModelId}' not exists.");
        }

        _modelRepository.Delete(entity);
        await _repositoryUnitOfWork.SaveAsync(cancellationToken);
    }
}
