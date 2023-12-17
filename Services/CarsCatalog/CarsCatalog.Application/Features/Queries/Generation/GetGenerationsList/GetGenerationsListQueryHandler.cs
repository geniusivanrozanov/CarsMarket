using CarsCatalog.Application.DTOs;
using CarsCatalog.Application.Interfaces.Repositories;
using MediatR;

namespace CarsCatalog.Application.Features.Queries;

public class GetGenerationsListQueryHandler :
    IRequestHandler<GetGenerationsListQuery, IEnumerable<GetGenerationDto>>
{
    private readonly IGenerationRepository _generationRepository;

    public GetGenerationsListQueryHandler(IRepositoryUnitOfWork repositoryUnitOfWork)
    {
        _generationRepository = repositoryUnitOfWork.Generations;
    }

    public async Task<IEnumerable<GetGenerationDto>> Handle(GetGenerationsListQuery request,
        CancellationToken cancellationToken)
    {
        var queryParameters = request.QueryParameters;
        
        var dto = await _generationRepository.GetGenerationsAsync<GetGenerationDto>(
            queryParameters.BrandId,
            queryParameters.BrandName,
            queryParameters.ModelId,
            queryParameters.ModelName,
            queryParameters.ProductionYear,
            cancellationToken);

        return dto;
    }
}
