using CarsCatalog.Application.DTOs;
using CarsCatalog.Application.Exceptions;
using CarsCatalog.Application.Interfaces.Repositories;
using CarsCatalog.Application.Mappers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarsCatalog.Application.Features.Commands;

public class CreateGenerationCommandHandler :
    IRequestHandler<CreateGenerationCommand, GetGenerationDto>
{
    private readonly IGenerationRepository _generationRepository;
    private readonly IRepositoryUnitOfWork _repositoryUnitOfWork;
    private readonly ILogger<CreateGenerationCommandHandler> _logger;

    public CreateGenerationCommandHandler(IRepositoryUnitOfWork repositoryUnitOfWork,
        ILogger<CreateGenerationCommandHandler> logger)
    {
        _repositoryUnitOfWork = repositoryUnitOfWork;
        _logger = logger;
        _generationRepository = repositoryUnitOfWork.Generations;
    }

    public async Task<GetGenerationDto> Handle(CreateGenerationCommand request, CancellationToken cancellationToken)
    {
        var entity = request.CreateGenerationDto.ToGenerationEntity();

        if (!await _repositoryUnitOfWork.Models.ExistsWithIdAsync(entity.ModelId, cancellationToken))
        {
            _logger.LogInformation("Model with id {Id} not exists", entity.ModelId);
            throw new NotExistsException($"Model with id '{entity.ModelId}' not exists.");
        }

        if (await _generationRepository.ExistsWithNameAndModelIdAsync(entity.Name, entity.ModelId, cancellationToken))
        {
            _logger.LogInformation("Generation with name '{Name}' already exists", entity.Name);
            throw new AlreadyExistsException($"Generation with name '{entity.Name}' already exists");
        }

        _generationRepository.Create(entity);
        await _repositoryUnitOfWork.SaveAsync(cancellationToken);

        var dto = entity.ToGetGenerationDto();

        return dto;
    }
}
