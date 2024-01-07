using FavoriteFilters.Application.DTOs.Filter;
using FavoriteFilters.Application.Features.Commands.Filter.UpdateFilter;
using FavoriteFilters.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FavoriteFilters.Application.Features.Queries.Filter.GetFiltersList;

public class GetFiltersListQueryHandler : IRequestHandler<GetFiltersListQuery, IEnumerable<GetFilterDto>>
{
    private readonly IFilterRepository _filterRepository;

    public GetFiltersListQueryHandler(IRepositoryUnitOfWork repositoryUnitOfWork)
    {
        _filterRepository = repositoryUnitOfWork.Filters;
    }
    
    public async Task<IEnumerable<GetFilterDto>> Handle(GetFiltersListQuery request, CancellationToken cancellationToken)
    {
        var dto = await _filterRepository.GetFiltersAsync<GetFilterDto>(cancellationToken);

        return dto;
    }
}
