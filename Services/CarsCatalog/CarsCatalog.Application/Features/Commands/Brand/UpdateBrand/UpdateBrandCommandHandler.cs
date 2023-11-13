using CarsCatalog.Application.DTOs;
using CarsCatalog.Application.Exceptions;
using CarsCatalog.Application.Interfaces.Repositories;
using CarsCatalog.Application.Mappers;
using CarsCatalog.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarsCatalog.Application.Features.Commands;

public class UpdateBrandCommandHandler(
    IRepositoryUnitOfWork repositoryUnitOfWork,
    ILogger<UpdateBrandCommandHandler> logger) :
    IRequestHandler<UpdateBrandCommand, GetBrandDto>
{
    private readonly IBrandRepository _brandRepository = repositoryUnitOfWork.Brands;

    public async Task<GetBrandDto> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
    {
        var entity = await _brandRepository.GetBrandByIdAsync<BrandEntity>(request.BrandId, cancellationToken);
        
        if (entity is null)
        {
            logger.LogInformation("Brand with id {Id} not exists", request.BrandId);
            throw new NotExistsException($"Brand with id '{request.BrandId}' not exists.");
        }

        request.UpdateBrandDto.ToBrandEntity(entity);
        entity.Id = request.BrandId;

        _brandRepository.UpdateBrand(entity);
        await repositoryUnitOfWork.SaveAsync(cancellationToken);

        var dto = entity.ToGetBrandDto();

        return dto;
    }
}
