using CarsCatalog.Application.DTOs;
using CarsCatalog.Application.Exceptions;
using CarsCatalog.Application.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using MediatR;

namespace CarsCatalog.Application.Features.Queries;

public class GetBrandByIdQueryHandler(
    IRepositoryUnitOfWork repositoryUnitOfWork,
    ILogger<GetBrandByIdQueryHandler> logger) :
    IRequestHandler<GetBrandByIdQuery, GetBrandDto>
{
    private readonly IBrandRepository _brandRepository = repositoryUnitOfWork.Brands;

    public async Task<GetBrandDto> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
    {
        var dto = await _brandRepository.GetBrandByIdAsync<GetBrandDto>(request.BrandId, cancellationToken);

        if (dto is null)
        {
            logger.LogInformation("Brand with id {Id} not exists", request.BrandId);
            throw new NotExistsException($"Brand with id '{request.BrandId}' not exists.");
        }

        return dto;
    }
}
