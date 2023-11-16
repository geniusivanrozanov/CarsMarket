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
    public async Task<IActionResult> GetModels(Guid? brandId, string? brandName)
    {
        var dto = await mediator.Send(new GetModelsListQuery
        {
            BrandId = brandId,
            BrandName = brandName
        });

        return Ok(dto);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetModelById(Guid id)
    {
        var dto = await mediator.Send(new GetModelByIdQuery(id));

        return Ok(dto);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateModel([FromBody]CreateModelDto createModelDto)
    {
        var dto = await mediator.Send(new CreateModelCommand(createModelDto));

        return CreatedAtAction(nameof(GetModelById), new {dto.Id}, dto);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateModel(Guid id, [FromBody]UpdateModelDto updateModelDto)
    {
        var dto = await mediator.Send(new UpdateModelCommand(id, updateModelDto));

        return CreatedAtAction(nameof(GetModelById), new {dto.Id}, dto);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteModel(Guid id)
    {
        await mediator.Send(new DeleteModelCommand(id));

        return NoContent();
    }
}
