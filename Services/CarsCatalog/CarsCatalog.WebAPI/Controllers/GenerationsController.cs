using CarsCatalog.Application.DTOs;
using CarsCatalog.Application.Features.Commands;
using CarsCatalog.Application.Features.Queries;
using CarsCatalog.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarsCatalog.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenerationsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetGenerations(
        Guid? brandId,
        string? brandName,
        Guid? modelId,
        string? modelName,
        int? productionYear,
        CancellationToken cancellationToken)
    {
        var dto = await mediator.Send(new GetGenerationsListQuery
        {
            BrandId = brandId,
            BrandName = brandName,
            ModelId = modelId,
            ModelName = modelName,
            ProductionYear = productionYear
        }, cancellationToken);

        return Ok(dto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetGenerationById(Guid id, CancellationToken cancellationToken)
    {
        var dto = await mediator.Send(new GetGenerationByIdQuery(id), cancellationToken);

        return Ok(dto);
    }

    [HttpPost]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> CreateGeneration([FromBody] CreateGenerationDto createGenerationDto,
        CancellationToken cancellationToken)
    {
        var dto = await mediator.Send(new CreateGenerationCommand(createGenerationDto), cancellationToken);

        return CreatedAtAction(nameof(GetGenerationById), new { dto.Id }, dto);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> UpdateGeneration(Guid id, [FromBody] UpdateGenerationDto updateGenerationDto,
        CancellationToken cancellationToken)
    {
        var dto = await mediator.Send(new UpdateGenerationCommand(id, updateGenerationDto), cancellationToken);

        return CreatedAtAction(nameof(GetGenerationById), new { dto.Id }, dto);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> DeleteGeneration(Guid id, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteGenerationCommand(id), cancellationToken);

        return NoContent();
    }
}
