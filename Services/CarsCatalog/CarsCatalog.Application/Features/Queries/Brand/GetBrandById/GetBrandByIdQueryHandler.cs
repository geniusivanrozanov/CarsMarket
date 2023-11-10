using CarsCatalog.Application.DTOs;
using CarsCatalog.Application.Interfaces.Mappers;
using CarsCatalog.Application.Interfaces.Repositories;
using CarsCatalog.Domain.Entities;
using Microsoft.Extensions.Logging;
using MediatR;

namespace CarsCatalog.Application.Features.Queries;

public class GetBrandByIdQueryHandler(
    IRepositoryUnitOfWork repositoryUnitOfWork,
    IMapper mapper,
    ILogger<GetBrandByIdQueryHandler> logger) :
    IRequestHandler<GetBrandByIdQuery, GetBrandDto>
{
    public async Task<GetBrandDto> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
    {
        var dto = await repositoryUnitOfWork.Brands
            .GetBrandByIdAsync(request.BrandId, mapper.Project<GetBrandDto, BrandEntity>, cancellationToken);

        if (dto is null)
        {
            logger.LogInformation("Brand with id {Id} not exists", request.BrandId);
            throw new Exception($"Brand with id '{request.BrandId}' not exists.");
        }

        return dto;
    }
}
