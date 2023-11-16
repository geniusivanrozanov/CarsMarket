using CarsCatalog.Application.DTOs;
using CarsCatalog.Application.Features.Commands;
using CarsCatalog.Application.Features.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarsCatalog.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ModelsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetModels(Guid? brandId, string? brandName, CancellationToken cancellationToken)
    {
        var dto = await mediator.Send(new GetModelsListQuery
        {
            BrandId = brandId,
            BrandName = brandName
        }, cancellationToken);

        return Ok(dto);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetModelById(Guid id, CancellationToken cancellationToken)
    {
        var dto = await mediator.Send(new GetModelByIdQuery(id), cancellationToken);

        return Ok(dto);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateModel([FromBody]CreateModelDto createModelDto, CancellationToken cancellationToken)
    {
        var dto = await mediator.Send(new CreateModelCommand(createModelDto), cancellationToken);

        return CreatedAtAction(nameof(GetModelById), new {dto.Id}, dto);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateModel(Guid id, [FromBody]UpdateModelDto updateModelDto, CancellationToken cancellationToken)
    {
        var dto = await mediator.Send(new UpdateModelCommand(id, updateModelDto), cancellationToken);

        return CreatedAtAction(nameof(GetModelById), new {dto.Id}, dto);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteModel(Guid id, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteModelCommand(id), cancellationToken);

        return NoContent();
    }
}
