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
    public async Task<IActionResult> GetBrands(CancellationToken cancellationToken)
    {
        var dto = await mediator.Send(new GetBrandsListQuery(), cancellationToken);

        return Ok(dto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBrandById(Guid id, CancellationToken cancellationToken)
    {
        var dto = await mediator.Send(new GetBrandByIdQuery(id), cancellationToken);

        return Ok(dto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBrand([FromBody] CreateBrandDto createBrandDto,
        CancellationToken cancellationToken)
    {
        var dto = await mediator.Send(new CreateBrandCommand(createBrandDto), cancellationToken);

        return CreatedAtAction(nameof(GetBrandById), new { dto.Id }, dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBrand(Guid id, [FromBody] UpdateBrandDto updateBrandDto,
        CancellationToken cancellationToken)
    {
        var dto = await mediator.Send(new UpdateBrandCommand(id, updateBrandDto), cancellationToken);

        return CreatedAtAction(nameof(GetBrandById), new { dto.Id }, dto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBrand(Guid id, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteBrandCommand(id), cancellationToken);

        return NoContent();
    }
}
