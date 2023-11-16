using CarsCatalog.Application.Exceptions;
using CarsCatalog.Application.Interfaces.Repositories;
using CarsCatalog.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarsCatalog.Application.Features.Commands;

public class DeleteGenerationCommandHandler(
    IRepositoryUnitOfWork repositoryUnitOfWork,
    ILogger<DeleteGenerationCommandHandler> logger) :
    IRequestHandler<DeleteGenerationCommand>
{
    private readonly IGenerationRepository _generationRepository = repositoryUnitOfWork.Generations;

    public async Task Handle(DeleteGenerationCommand request, CancellationToken cancellationToken)
    {
        var entity =
            await _generationRepository.GetGenerationByIdAsync<GenerationEntity>(request.GenerationId,
                cancellationToken);

        if (entity is null)
        {
            logger.LogInformation("Generation with id {Id} not exists", request.GenerationId);
            throw new NotExistsException($"Generation with id '{request.GenerationId}' not exists.");
        }

        _generationRepository.DeleteGeneration(entity);
        await repositoryUnitOfWork.SaveAsync(cancellationToken);
    }
}
