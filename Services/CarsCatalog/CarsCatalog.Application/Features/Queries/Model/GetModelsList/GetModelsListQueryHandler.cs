using CarsCatalog.Application.DTOs;
using CarsCatalog.Application.Interfaces.Repositories;
using MediatR;

namespace CarsCatalog.Application.Features.Queries;

public class GetModelsListQueryHandler :
    IRequestHandler<GetModelsListQuery, IEnumerable<GetModelDto>>
{
    private readonly IModelRepository _modelRepository;

    public GetModelsListQueryHandler(IRepositoryUnitOfWork repositoryUnitOfWork)
    {
        _modelRepository = repositoryUnitOfWork.Models;
    }

    public async Task<IEnumerable<GetModelDto>> Handle(GetModelsListQuery request, CancellationToken cancellationToken)
    {
        var queryParameters = request.QueryParameters;
        
        var dto = await _modelRepository.GetModelsAsync<GetModelDto>(
            queryParameters.BrandId,
            queryParameters.BrandName,
            cancellationToken);

        return dto;
    }
}
