using CarsCatalog.Application.Exceptions;
using CarsCatalog.Application.Interfaces.Repositories;
using CarsCatalog.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarsCatalog.Application.Features.Commands;

public class DeleteGenerationCommandHandler :
    IRequestHandler<DeleteGenerationCommand>
{
    private readonly IGenerationRepository _generationRepository;
    private readonly IRepositoryUnitOfWork _repositoryUnitOfWork;
    private readonly ILogger<DeleteGenerationCommandHandler> _logger;

    public DeleteGenerationCommandHandler(IRepositoryUnitOfWork repositoryUnitOfWork,
        ILogger<DeleteGenerationCommandHandler> logger)
    {
        _repositoryUnitOfWork = repositoryUnitOfWork;
        _logger = logger;
        _generationRepository = repositoryUnitOfWork.Generations;
    }

    public async Task Handle(DeleteGenerationCommand request, CancellationToken cancellationToken)
    {
        var entity =
            await _generationRepository.GetGenerationByIdAsync<GenerationEntity>(request.GenerationId,
                cancellationToken);

        if (entity is null)
        {
            _logger.LogInformation("Generation with id {Id} not exists", request.GenerationId);
            throw new NotExistsException($"Generation with id '{request.GenerationId}' not exists.");
        }

        _generationRepository.Delete(entity);
        await _repositoryUnitOfWork.SaveAsync(cancellationToken);
    }
}
