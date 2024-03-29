﻿using CarsCatalog.Application.DTOs;
using CarsCatalog.Application.Exceptions;
using CarsCatalog.Application.Interfaces.Repositories;
using CarsCatalog.Application.Mappers;
using CarsCatalog.Domain.Entities;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CarsCatalog.Application.Features.Commands;

public class UpdateBrandCommandHandler :
    IRequestHandler<UpdateBrandCommand, GetBrandDto>
{
    private readonly IBrandRepository _brandRepository;
    private readonly IRepositoryUnitOfWork _repositoryUnitOfWork;
    private readonly ILogger<UpdateBrandCommandHandler> _logger;
    private readonly IPublishEndpoint _publishEndpoint;

    public UpdateBrandCommandHandler(IRepositoryUnitOfWork repositoryUnitOfWork,
        ILogger<UpdateBrandCommandHandler> logger, IPublishEndpoint publishEndpoint)
    {
        _repositoryUnitOfWork = repositoryUnitOfWork;
        _logger = logger;
        _publishEndpoint = publishEndpoint;
        _brandRepository = repositoryUnitOfWork.Brands;
    }

    public async Task<GetBrandDto> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
    {
        var entity = await _brandRepository.GetBrandByIdAsync<BrandEntity>(request.BrandId, cancellationToken);

        if (entity is null)
        {
            _logger.LogInformation("Brand with id {Id} not exists", request.BrandId);
            throw new NotExistsException($"Brand with id '{request.BrandId}' not exists.");
        }

        if (entity.Name != request.UpdateBrandDto.Name &&
            await _brandRepository.ExistsWithNameAsync(request.UpdateBrandDto.Name, cancellationToken))
        {
            _logger.LogInformation("Brand with name '{Name}' already exists", entity.Name);
            throw new AlreadyExistsException($"Brand with name '{entity.Name}' already exists");
        }

        request.UpdateBrandDto.ToBrandEntity(entity);
        entity.Id = request.BrandId;

        _brandRepository.Update(entity);
        await _repositoryUnitOfWork.SaveAsync(cancellationToken);

        var message = entity.ToBrandUpdatedMessage();
        await _publishEndpoint.Publish(message, cancellationToken);

        var dto = entity.ToGetBrandDto();

        return dto;
    }
}
