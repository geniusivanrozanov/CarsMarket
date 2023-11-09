using CarsCatalog.Application.DTOs;
using CarsCatalog.Application.Interfaces.Mappers;
using CarsCatalog.Application.Interfaces.Repositories;
using CarsCatalog.Domain.Entities;
using MediatR;

namespace CarsCatalog.Application.Features.Commands;

public class CreateBrandCommandHandler(
    IRepositoryUnitOfWork repositoryUnitOfWork,
    IMapper mapper) :
    IRequestHandler<CreateBrandCommand, GetBrandDto>
{
    public async Task<GetBrandDto> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<BrandEntity, CreateBrandDto>(request.CreateBrandDto);
        
        repositoryUnitOfWork.Brands.CreateBrand(entity);
        await repositoryUnitOfWork.SaveAsync(cancellationToken);

        var dto = mapper.Map<GetBrandDto, BrandEntity>(entity);

        return dto;
    }
}
