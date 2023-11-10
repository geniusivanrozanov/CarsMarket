using CarsCatalog.Application.DTOs;
using CarsCatalog.Application.Interfaces.Repositories;
using CarsCatalog.Application.Mappers;
using MediatR;

namespace CarsCatalog.Application.Features.Commands;

public class CreateBrandCommandHandler(IRepositoryUnitOfWork repositoryUnitOfWork) :
    IRequestHandler<CreateBrandCommand, GetBrandDto>
{
    private readonly IBrandRepository _brandRepository = repositoryUnitOfWork.Brands;
    
    public async Task<GetBrandDto> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        var entity = request.CreateBrandDto.ToBrandEntity();
        
        _brandRepository.CreateBrand(entity);
        await repositoryUnitOfWork.SaveAsync(cancellationToken);

        var dto = entity.ToGetBrandDto();

        return dto;
    }
}
