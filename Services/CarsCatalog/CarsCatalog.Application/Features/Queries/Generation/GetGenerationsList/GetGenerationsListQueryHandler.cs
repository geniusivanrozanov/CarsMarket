using CarsCatalog.Application.DTOs;
using CarsCatalog.Application.Interfaces.Repositories;
using MediatR;

namespace CarsCatalog.Application.Features.Queries;

public class GetGenerationsListQueryHandler(IRepositoryUnitOfWork repositoryUnitOfWork) :
    IRequestHandler<GetGenerationsListQuery, IEnumerable<GetGenerationDto>>
{
    private readonly IGenerationRepository _generationRepository = repositoryUnitOfWork.Generations;

    public async Task<IEnumerable<GetGenerationDto>> Handle(GetGenerationsListQuery request,
        CancellationToken cancellationToken)
    {
        var dto = await _generationRepository.GetGenerationsAsync<GetGenerationDto>(
            request.BrandId,
            request.BrandName,
            request.ModelId,
            request.ModelName,
            request.ProductionYear,
            cancellationToken);

        return dto;
    }
}
