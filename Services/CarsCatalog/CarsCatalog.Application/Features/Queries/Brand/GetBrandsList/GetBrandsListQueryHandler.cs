using CarsCatalog.Application.DTOs;
using CarsCatalog.Application.Interfaces.Repositories;
using MediatR;

namespace CarsCatalog.Application.Features.Queries;

public class GetBrandsListQueryHandler(IRepositoryUnitOfWork repositoryUnitOfWork) :
    IRequestHandler<GetBrandsListQuery, IEnumerable<GetBrandDto>>
{
    private readonly IBrandRepository _brandRepository = repositoryUnitOfWork.Brands;

    public async Task<IEnumerable<GetBrandDto>> Handle(GetBrandsListQuery request, CancellationToken cancellationToken)
    {
        var dto = await _brandRepository.GetBrandsAsync<GetBrandDto>(cancellationToken);

        return dto;
    }
}
