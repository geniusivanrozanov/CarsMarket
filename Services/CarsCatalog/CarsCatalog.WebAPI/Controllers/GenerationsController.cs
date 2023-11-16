using CarsCatalog.Application.DTOs;
using CarsCatalog.Application.Features.Commands;
using CarsCatalog.Application.Features.Queries;
using MediatR;
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
        int? productionYear)
    {
        var dto = await mediator.Send(new GetGenerationsListQuery
        {
            BrandId =  brandId,
            BrandName = brandName,
            ModelId = modelId,
            ModelName = modelName,
            ProductionYear = productionYear
        });

        return Ok(dto);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetGenerationById(Guid id)
    {
        var dto = await mediator.Send(new GetGenerationByIdQuery(id));

        return Ok(dto);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateGeneration([FromBody]CreateGenerationDto createGenerationDto)
    {
        var dto = await mediator.Send(new CreateGenerationCommand(createGenerationDto));

        return CreatedAtAction(nameof(GetGenerationById), new {dto.Id}, dto);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGeneration(Guid id, [FromBody]UpdateGenerationDto updateGenerationDto)
    {
        var dto = await mediator.Send(new UpdateGenerationCommand(id, updateGenerationDto));

        return CreatedAtAction(nameof(GetGenerationById), new {dto.Id}, dto);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGeneration(Guid id)
    {
        await mediator.Send(new DeleteGenerationCommand(id));

        return NoContent();
    }
}
