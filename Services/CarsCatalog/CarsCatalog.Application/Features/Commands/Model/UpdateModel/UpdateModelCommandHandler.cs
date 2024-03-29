﻿using CarsCatalog.Application.DTOs;
using CarsCatalog.Application.Exceptions;
using CarsCatalog.Application.Interfaces.Repositories;
using CarsCatalog.Application.Mappers;
using CarsCatalog.Domain.Entities;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarsCatalog.Application.Features.Commands;

public class UpdateModelCommandHandler :
    IRequestHandler<UpdateModelCommand, GetModelDto>
{
    private readonly IModelRepository _modelRepository;
    private readonly IRepositoryUnitOfWork _repositoryUnitOfWork;
    private readonly ILogger<UpdateModelCommandHandler> _logger;
    private readonly IPublishEndpoint _publishEndpoint;

    public UpdateModelCommandHandler(IRepositoryUnitOfWork repositoryUnitOfWork,
        ILogger<UpdateModelCommandHandler> logger, IPublishEndpoint publishEndpoint)
    {
        _repositoryUnitOfWork = repositoryUnitOfWork;
        _logger = logger;
        _publishEndpoint = publishEndpoint;
        _modelRepository = repositoryUnitOfWork.Models;
    }

    public async Task<GetModelDto> Handle(UpdateModelCommand request, CancellationToken cancellationToken)
    {
        var entity = await _modelRepository.GetModelByIdAsync<ModelEntity>(request.ModelId, cancellationToken);

        if (entity is null)
        {
            _logger.LogInformation("Model with id {Id} not exists", request.ModelId);
            throw new NotExistsException($"Model with id '{request.ModelId}' not exists.");
        }

        if (entity.Name != request.UpdateModelDto.Name &&
            await _modelRepository.ExistsWithNameAndBrandIdAsync(request.UpdateModelDto.Name, entity.BrandId,
                cancellationToken))
        {
            _logger.LogInformation("Model with name '{Name}' already exists", entity.Name);
            throw new AlreadyExistsException($"Model with name '{entity.Name}' already exists");
        }

        request.UpdateModelDto.ToModelEntity(entity);
        entity.Id = request.ModelId;

        _modelRepository.Update(entity);
        await _repositoryUnitOfWork.SaveAsync(cancellationToken);

        var message = entity.ToModelUpdatedMessage();
        await _publishEndpoint.Publish(message, cancellationToken);

        var dto = entity.ToGetModelDto();

        return dto;
    }
}
