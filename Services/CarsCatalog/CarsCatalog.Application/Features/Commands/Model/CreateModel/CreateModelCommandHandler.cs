using CarsCatalog.Application.DTOs;
using CarsCatalog.Application.Exceptions;
using CarsCatalog.Application.Interfaces.Repositories;
using CarsCatalog.Application.Mappers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarsCatalog.Application.Features.Commands;

public class CreateModelCommandHandler(
    IRepositoryUnitOfWork repositoryUnitOfWork,
    ILogger<CreateModelCommandHandler> logger) :
    IRequestHandler<CreateModelCommand, GetModelDto>
{
    private readonly IModelRepository _modelRepository = repositoryUnitOfWork.Models;
    
    public async Task<GetModelDto> Handle(CreateModelCommand request, CancellationToken cancellationToken)
    {
        var entity = request.CreateModelDto.ToModelEntity();

        if (!await repositoryUnitOfWork.Brands.ExistsWithIdAsync(request.CreateModelDto.BrandId, cancellationToken))
        {
            logger.LogInformation("Brand with id {Id} not exists", request.CreateModelDto.BrandId);
            throw new NotExistsException($"Brand with id '{request.CreateModelDto.BrandId}' not exists.");
        }

        if (await _modelRepository.ExistsWithNameAndBrandIdAsync(entity.Name, entity.BrandId, cancellationToken))
        {
            logger.LogInformation("Model with name '{Name}' already exists", entity.Name);
            throw new AlreadyExistsException($"Model with name '{entity.Name}' already exists");
        }
        
        _modelRepository.CreateModel(entity);
        await repositoryUnitOfWork.SaveAsync(cancellationToken);

        var dto = entity.ToGetModelDto();

        return dto;
    }
}
