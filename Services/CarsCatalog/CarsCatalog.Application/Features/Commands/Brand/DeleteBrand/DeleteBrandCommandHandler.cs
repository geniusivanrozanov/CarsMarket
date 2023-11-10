using CarsCatalog.Application.Exceptions;
using CarsCatalog.Application.Interfaces.Mappers;
using CarsCatalog.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarsCatalog.Application.Features.Commands;

public class DeleteBrandCommandHandler(
    IRepositoryUnitOfWork repositoryUnitOfWork,
    ILogger<DeleteBrandCommandHandler> logger) :
    IRequestHandler<DeleteBrandCommand>
{
    public async Task Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
    {
        var entity = await repositoryUnitOfWork.Brands
            .GetBrandByIdAsync(request.BrandId, entities => entities, cancellationToken);
        
        if (entity is null)
        {
            logger.LogInformation("Brand with id {Id} not exists", request.BrandId);
            throw new NotExistsException($"Brand with id '{request.BrandId}' not exists.");
        }
        
        repositoryUnitOfWork.Brands
            .DeleteBrand(entity);
        await repositoryUnitOfWork.SaveAsync(cancellationToken);
    }
}
