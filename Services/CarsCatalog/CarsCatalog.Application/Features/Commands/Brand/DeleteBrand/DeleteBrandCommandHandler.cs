using CarsCatalog.Application.Exceptions;
using CarsCatalog.Application.Interfaces.Repositories;
using CarsCatalog.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarsCatalog.Application.Features.Commands;

public class DeleteBrandCommandHandler :
    IRequestHandler<DeleteBrandCommand>
{
    private readonly IBrandRepository _brandRepository;
    private readonly IRepositoryUnitOfWork _repositoryUnitOfWork;
    private readonly ILogger<DeleteBrandCommandHandler> _logger;

    public DeleteBrandCommandHandler(IRepositoryUnitOfWork repositoryUnitOfWork,
        ILogger<DeleteBrandCommandHandler> logger)
    {
        _repositoryUnitOfWork = repositoryUnitOfWork;
        _logger = logger;
        _brandRepository = repositoryUnitOfWork.Brands;
    }

    public async Task Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
    {
        var entity = await _brandRepository.GetBrandByIdAsync<BrandEntity>(request.BrandId, cancellationToken);

        if (entity is null)
        {
            _logger.LogInformation("Brand with id {Id} not exists", request.BrandId);
            throw new NotExistsException($"Brand with id '{request.BrandId}' not exists.");
        }

        _brandRepository.DeleteBrand(entity);
        await _repositoryUnitOfWork.SaveAsync(cancellationToken);
    }
}
