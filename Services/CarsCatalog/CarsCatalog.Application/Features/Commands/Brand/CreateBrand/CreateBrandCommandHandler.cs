using CarsCatalog.Application.DTOs;
using CarsCatalog.Application.Exceptions;
using CarsCatalog.Application.Interfaces.Repositories;
using CarsCatalog.Application.Mappers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarsCatalog.Application.Features.Commands;

public class CreateBrandCommandHandler(
    IRepositoryUnitOfWork repositoryUnitOfWork,
    ILogger<CreateBrandCommandHandler> logger) :
    IRequestHandler<CreateBrandCommand, GetBrandDto>
{
    private readonly IBrandRepository _brandRepository = repositoryUnitOfWork.Brands;

    public async Task<GetBrandDto> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        var entity = request.CreateBrandDto.ToBrandEntity();

        if (await _brandRepository.ExistsWithNameAsync(entity.Name, cancellationToken))
        {
            logger.LogInformation("Brand with name '{Name}' already exists", entity.Name);
            throw new AlreadyExistsException($"Brand with name '{entity.Name}' already exists");
        }

        _brandRepository.CreateBrand(entity);
        await repositoryUnitOfWork.SaveAsync(cancellationToken);

        var dto = entity.ToGetBrandDto();

        return dto;
    }
}
