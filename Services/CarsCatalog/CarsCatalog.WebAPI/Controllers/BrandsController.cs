using CarsCatalog.Application.DTOs;
using CarsCatalog.Application.Features.Commands;
using CarsCatalog.Application.Features.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarsCatalog.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BrandsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetBrands()
    {
        var dto = await mediator.Send(new GetBrandsListQuery());

        return Ok(dto);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBrandById(Guid id)
    {
        var dto = await mediator.Send(new GetBrandByIdQuery(id));

        return Ok(dto);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateBrand([FromBody]CreateBrandDto createBrandDto)
    {
        var dto = await mediator.Send(new CreateBrandCommand(createBrandDto));

        return CreatedAtAction(nameof(GetBrandById), new {dto.Id}, dto);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBrand(Guid id, [FromBody]UpdateBrandDto updateBrandDto)
    {
        var dto = await mediator.Send(new UpdateBrandCommand(id, updateBrandDto));

        return CreatedAtAction(nameof(GetBrandById), new {dto.Id}, dto);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBrand(Guid id)
    {
        await mediator.Send(new DeleteBrandCommand(id));

        return NoContent();
    }
}
