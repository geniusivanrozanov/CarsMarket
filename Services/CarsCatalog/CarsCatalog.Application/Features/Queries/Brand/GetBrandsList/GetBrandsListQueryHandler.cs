using CarsCatalog.Application.DTOs;
using CarsCatalog.Application.Interfaces.Repositories;
using MediatR;

namespace CarsCatalog.Application.Features.Queries;

public class GetBrandsListQueryHandler :
    IRequestHandler<GetBrandsListQuery, IEnumerable<GetBrandDto>>
{
    private readonly IBrandRepository _brandRepository;

    public GetBrandsListQueryHandler(IRepositoryUnitOfWork repositoryUnitOfWork)
    {
        _brandRepository = repositoryUnitOfWork.Brands;
    }

    public async Task<IEnumerable<GetBrandDto>> Handle(GetBrandsListQuery request, CancellationToken cancellationToken)
    {
        var dto = await _brandRepository.GetBrandsAsync<GetBrandDto>(cancellationToken);

        return dto;
    }
}
