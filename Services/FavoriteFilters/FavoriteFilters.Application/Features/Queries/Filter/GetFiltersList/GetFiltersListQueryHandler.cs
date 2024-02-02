using FavoriteFilters.Application.DTOs.Filter;
using FavoriteFilters.Application.Features.Commands.Filter.UpdateFilter;
using FavoriteFilters.Application.Interfaces.Repositories;
using FavoriteFilters.Application.Interfaces.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FavoriteFilters.Application.Features.Queries.Filter.GetFiltersList;

public class GetFiltersListQueryHandler : IRequestHandler<GetFiltersListQuery, IEnumerable<GetFilterDto>>
{
    private readonly IFilterRepository _filterRepository;
    private readonly ICurrentUser _currentUser;

    public GetFiltersListQueryHandler(IRepositoryUnitOfWork repositoryUnitOfWork, ICurrentUser currentUser)
    {
        _currentUser = currentUser;
        _filterRepository = repositoryUnitOfWork.Filters;
    }
    
    public async Task<IEnumerable<GetFilterDto>> Handle(GetFiltersListQuery request, CancellationToken cancellationToken)
    {
        var dto = await _filterRepository.GetFiltersByUserIdAsync<GetFilterDto>(_currentUser.Id, cancellationToken);

        return dto;
    }
}
