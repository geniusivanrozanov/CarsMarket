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
public class GenerationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public GenerationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetGenerations([FromQuery] GetGenerationsListQueryParameters queryParameters, CancellationToken cancellationToken)
    {
        var dto = await _mediator.Send(new GetGenerationsListQuery(queryParameters), cancellationToken);

        return Ok(dto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetGenerationById(Guid id, CancellationToken cancellationToken)
    {
        var dto = await _mediator.Send(new GetGenerationByIdQuery(id), cancellationToken);

        return Ok(dto);
    }

    [HttpPost]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> CreateGeneration([FromBody] CreateGenerationDto createGenerationDto,
        CancellationToken cancellationToken)
    {
        var dto = await _mediator.Send(new CreateGenerationCommand(createGenerationDto), cancellationToken);

        return CreatedAtAction(nameof(GetGenerationById), new { dto.Id }, dto);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> UpdateGeneration(Guid id, [FromBody] UpdateGenerationDto updateGenerationDto,
        CancellationToken cancellationToken)
    {
        var dto = await _mediator.Send(new UpdateGenerationCommand(id, updateGenerationDto), cancellationToken);

        return CreatedAtAction(nameof(GetGenerationById), new { dto.Id }, dto);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> DeleteGeneration(Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteGenerationCommand(id), cancellationToken);

        return NoContent();
    }
}
