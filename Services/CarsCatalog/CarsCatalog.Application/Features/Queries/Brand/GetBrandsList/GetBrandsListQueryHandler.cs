using CarsCatalog.Application.DTOs;
using CarsCatalog.Application.Interfaces.Mappers;
using CarsCatalog.Application.Interfaces.Repositories;
using CarsCatalog.Domain.Entities;
using MediatR;

namespace CarsCatalog.Application.Features.Queries;

public class GetBrandsListQueryHandler(
    IRepositoryUnitOfWork repositoryUnitOfWork,
    IMapper mapper) :
    IRequestHandler<GetBrandsListQuery, IEnumerable<GetBrandDto>>
{
    public async Task<IEnumerable<GetBrandDto>> Handle(GetBrandsListQuery request, CancellationToken cancellationToken)
    {
        var dto = await repositoryUnitOfWork.Brands.GetBrandsAsync(mapper.Project<GetBrandDto, BrandEntity>, cancellationToken);

        return dto;
    }
}
