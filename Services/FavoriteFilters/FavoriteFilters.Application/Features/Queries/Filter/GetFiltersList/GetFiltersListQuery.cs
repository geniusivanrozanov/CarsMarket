using FavoriteFilters.Application.DTOs.Filter;
using MediatR;

namespace FavoriteFilters.Application.Features.Queries.Filter.GetFiltersList;

public class GetFiltersListQuery : IRequest<IEnumerable<GetFilterDto>>;
