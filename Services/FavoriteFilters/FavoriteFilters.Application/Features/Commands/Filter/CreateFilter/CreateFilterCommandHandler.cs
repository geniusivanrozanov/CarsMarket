using FavoriteFilters.Application.DTOs.Filter;
using FavoriteFilters.Application.Interfaces.Repositories;
using FavoriteFilters.Application.Interfaces.Services;
using FavoriteFilters.Domain.Entities;
using Mapster;
using MapsterMapper;
using MediatR;

namespace FavoriteFilters.Application.Features.Commands.Filter.CreateFilter;

public class CreateFilterCommandHandler : IRequestHandler<CreateFilterCommand, GetFilterDto>
{
    private readonly IFilterRepository _filterRepository;
    private readonly IRepositoryUnitOfWork _repositoryUnitOfWork;
    private readonly ICurrentUser _currentUser;

    public CreateFilterCommandHandler(IRepositoryUnitOfWork repositoryUnitOfWork, ICurrentUser currentUser)
    {
        _repositoryUnitOfWork = repositoryUnitOfWork;
        _currentUser = currentUser;
        _filterRepository = repositoryUnitOfWork.Filters;
    }
    
    public async Task<GetFilterDto> Handle(CreateFilterCommand request, CancellationToken cancellationToken)
    {
        var entity = request.CreateFilterDto.Adapt<FilterEntity>();

        entity.UserId = _currentUser.Id;
        entity.UserEmail = _currentUser.Email;
        
        _filterRepository.Create(entity);
        await _repositoryUnitOfWork.SaveAsync(cancellationToken);

        var dto = entity.Adapt<GetFilterDto>();

        return dto;
    }
}
