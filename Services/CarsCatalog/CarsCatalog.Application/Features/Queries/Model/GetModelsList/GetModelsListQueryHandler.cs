using CarsCatalog.Application.DTOs;
using CarsCatalog.Application.Interfaces.Repositories;
using MediatR;

namespace CarsCatalog.Application.Features.Queries;

public class GetModelsListQueryHandler(IRepositoryUnitOfWork repositoryUnitOfWork) :
    IRequestHandler<GetModelsListQuery, IEnumerable<GetModelDto>>
{
    private readonly IModelRepository _modelRepository = repositoryUnitOfWork.Models;

    public async Task<IEnumerable<GetModelDto>> Handle(GetModelsListQuery request, CancellationToken cancellationToken)
    {
        var dto = await _modelRepository.GetModelsAsync<GetModelDto>(
            request.BrandId,
            request.BrandName,
            cancellationToken);

        return dto;
    }
}
