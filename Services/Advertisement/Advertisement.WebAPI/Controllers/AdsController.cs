using Advertisement.Application.DTOs.Ad;
using Advertisement.Application.Features.Commands.CreateAd;
using Advertisement.Application.Features.Commands.DeleteAd;
using Advertisement.Application.Features.Commands.UpdateAd;
using Advertisement.Application.Features.Commands.UpdateAdStatus;
using Advertisement.Application.Features.Queries.GetAdById;
using Advertisement.Application.Features.Queries.GetAdsList;
using Advertisement.Application.QueryParameters;
using Advertisement.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Advertisement.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAds([FromQuery] AdQueryParameters adQueryParameters, CancellationToken cancellationToken)
    {
        var dto = await _mediator.Send(new GetAdsListQuery(adQueryParameters), cancellationToken);

        return Ok(dto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAdById(Guid id, CancellationToken cancellationToken)
    {
        var dto = await _mediator.Send(new GetAdByIdQuery(id), cancellationToken);

        return Ok(dto);
    }
    
    [HttpGet("{id}/price-history")]
    public async Task<IActionResult> GetAdPriceHistoryById(Guid id, CancellationToken cancellationToken)
    {
        var dto = await _mediator.Send(new GetAdPriceHistoryByIdQuery(id), cancellationToken);

        return Ok(dto);
    }
    
    [HttpPost]
    [Authorize(Roles = Roles.User)]
    public async Task<IActionResult> CreateAd([FromBody] CreateAdDto createAdDto,
        CancellationToken cancellationToken)
    {
        var dto = await _mediator.Send(new CreateAdCommand(createAdDto), cancellationToken);
        
        return CreatedAtAction(nameof(GetAdById), new { dto.Id }, dto);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = Roles.User)]
    public async Task<IActionResult> UpdateAd(Guid id, [FromBody] UpdateAdDto updateAdDto,
        CancellationToken cancellationToken)
    {
        var dto = await _mediator.Send(new UpdateAdCommand(id, updateAdDto), cancellationToken);

        return CreatedAtAction(nameof(GetAdById), new { dto.Id }, dto);
    }
    
    [HttpPut("{id}/status")]
    [Authorize(Roles = $"{Roles.Moderator}, {Roles.Admin}")]
    public async Task<IActionResult> UpdateAdStatus(Guid id, [FromBody] UpdateAdStatusDto updateAdStatusDto,
        CancellationToken cancellationToken)
    {
        var dto = await _mediator.Send(new UpdateAdStatusCommand(id, updateAdStatusDto), cancellationToken);

        return CreatedAtAction(nameof(GetAdById), new { dto.Id }, dto);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = $"{Roles.User}, {Roles.Admin}")]
    public async Task<IActionResult> DeleteAd(Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteAdCommand(id), cancellationToken);

        return NoContent();
    }
}
