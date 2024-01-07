using FavoriteFilters.Application.DTOs.Filter;
using FavoriteFilters.Application.Interfaces.Repositories;
using FavoriteFilters.Domain.Entities;
using Mapster;
using MapsterMapper;
using MediatR;

namespace FavoriteFilters.Application.Features.Commands.Filter.CreateFilter;

public class CreateFilterCommandHandler : IRequestHandler<CreateFilterCommand, GetFilterDto>
{
    private readonly IFilterRepository _filterRepository;
    private readonly IRepositoryUnitOfWork _repositoryUnitOfWork;

    public CreateFilterCommandHandler(IRepositoryUnitOfWork repositoryUnitOfWork)
    {
        _repositoryUnitOfWork = repositoryUnitOfWork;
        _filterRepository = repositoryUnitOfWork.Filters;
    }
    
    public async Task<GetFilterDto> Handle(CreateFilterCommand request, CancellationToken cancellationToken)
    {
        var entity = request.CreateFilterDto.Adapt<FilterEntity>();
        
        _filterRepository.Create(entity);
        await _repositoryUnitOfWork.SaveAsync(cancellationToken);

        var dto = entity.Adapt<GetFilterDto>();

        return dto;
    }
}
