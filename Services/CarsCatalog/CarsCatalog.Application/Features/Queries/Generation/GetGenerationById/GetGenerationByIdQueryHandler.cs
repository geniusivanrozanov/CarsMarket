using CarsCatalog.Application.DTOs;
using CarsCatalog.Application.Exceptions;
using CarsCatalog.Application.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using MediatR;

namespace CarsCatalog.Application.Features.Queries;

public class GetGenerationByIdQueryHandler :
    IRequestHandler<GetGenerationByIdQuery, GetGenerationDto>
{
    private readonly IGenerationRepository _generationRepository;
    private readonly ILogger<GetGenerationByIdQueryHandler> _logger;

    public GetGenerationByIdQueryHandler(IRepositoryUnitOfWork repositoryUnitOfWork,
        ILogger<GetGenerationByIdQueryHandler> logger)
    {
        _logger = logger;
        _generationRepository = repositoryUnitOfWork.Generations;
    }

    public async Task<GetGenerationDto> Handle(GetGenerationByIdQuery request, CancellationToken cancellationToken)
    {
        var dto = await _generationRepository.GetGenerationByIdAsync<GetGenerationDto>(request.GenerationId, cancellationToken);

        if (dto is null)
        {
            _logger.LogInformation("Generation with id {Id} not exists", request.GenerationId);
            throw new NotExistsException($"Generation with id '{request.GenerationId}' not exists.");
        }

        return dto;
    }
}
