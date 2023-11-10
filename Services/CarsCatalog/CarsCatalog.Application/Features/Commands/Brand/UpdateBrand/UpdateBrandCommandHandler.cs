using CarsCatalog.Application.DTOs;
using CarsCatalog.Application.Exceptions;
using CarsCatalog.Application.Interfaces.Mappers;
using CarsCatalog.Application.Interfaces.Repositories;
using CarsCatalog.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarsCatalog.Application.Features.Commands;

public class UpdateBrandCommandHandler(
    IRepositoryUnitOfWork repositoryUnitOfWork,
    IMapper mapper,
    ILogger<UpdateBrandCommandHandler> logger) :
    IRequestHandler<UpdateBrandCommand, GetBrandDto>
{
    public async Task<GetBrandDto> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
    {
        var entity = await repositoryUnitOfWork.Brands
            .GetBrandByIdAsync(request.BrandId, entities => entities, cancellationToken);
        
        if (entity is null)
        {
            logger.LogInformation("Brand with id {Id} not exists", request.BrandId);
            throw new NotExistsException($"Brand with id '{request.BrandId}' not exists.");
        }

        var updatedEntity = mapper.Map<BrandEntity, UpdateBrandDto>(request.UpdateBrandDto);

        repositoryUnitOfWork.Brands
            .UpdateBrand(updatedEntity);
        await repositoryUnitOfWork.SaveAsync(cancellationToken);

        var dto = mapper.Map<GetBrandDto, BrandEntity>(updatedEntity);

        return dto;
    }
}
