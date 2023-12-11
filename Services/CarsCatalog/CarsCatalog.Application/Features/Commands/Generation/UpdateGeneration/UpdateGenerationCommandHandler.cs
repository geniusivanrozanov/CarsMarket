using CarsCatalog.Application.DTOs;
using CarsCatalog.Application.Exceptions;
using CarsCatalog.Application.Interfaces.Repositories;
using CarsCatalog.Application.Mappers;
using CarsCatalog.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarsCatalog.Application.Features.Commands;

public class UpdateGenerationCommandHandler :
    IRequestHandler<UpdateGenerationCommand, GetGenerationDto>
{
    private readonly IGenerationRepository _generationRepository;
    private readonly IRepositoryUnitOfWork _repositoryUnitOfWork;
    private readonly ILogger<UpdateGenerationCommandHandler> _logger;

    public UpdateGenerationCommandHandler(IRepositoryUnitOfWork repositoryUnitOfWork,
        ILogger<UpdateGenerationCommandHandler> logger)
    {
        _repositoryUnitOfWork = repositoryUnitOfWork;
        _logger = logger;
        _generationRepository = repositoryUnitOfWork.Generations;
    }

    public async Task<GetGenerationDto> Handle(UpdateGenerationCommand request, CancellationToken cancellationToken)
    {
        var entity =
            await _generationRepository.GetGenerationByIdAsync<GenerationEntity>(request.GenerationId,
                cancellationToken);

        if (entity is null)
        {
            _logger.LogInformation("Generation with id {Id} not exists", request.GenerationId);
            throw new NotExistsException($"Generation with id '{request.GenerationId}' not exists.");
        }

        if (entity.Name != request.UpdateGenerationDto.Name &&
            await _generationRepository.ExistsWithNameAndModelIdAsync(request.UpdateGenerationDto.Name, entity.ModelId,
                cancellationToken))
        {
            _logger.LogInformation("Generation with name '{Name}' already exists", entity.Name);
            throw new AlreadyExistsException($"Generation with name '{entity.Name}' already exists");
        }

        request.UpdateGenerationDto.ToGenerationEntity(entity);
        entity.Id = request.GenerationId;

        _generationRepository.Update(entity);
        await _repositoryUnitOfWork.SaveAsync(cancellationToken);

        var dto = entity.ToGetGenerationDto();

        return dto;
    }
}
