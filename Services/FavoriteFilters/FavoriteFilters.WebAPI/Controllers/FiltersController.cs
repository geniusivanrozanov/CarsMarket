using FavoriteFilters.Application.DTOs.Filter;
using FavoriteFilters.Application.Features.Commands.Filter.CreateFilter;
using FavoriteFilters.Application.Features.Commands.Filter.DeleteFilter;
using FavoriteFilters.Application.Features.Commands.Filter.UpdateFilter;
using FavoriteFilters.Application.Features.Queries.Filter.GetFilterById;
using FavoriteFilters.Application.Features.Queries.Filter.GetFiltersList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FavoriteFilters.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FiltersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FiltersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetFilters(CancellationToken cancellationToken)
        {
            var dto = await _mediator.Send(new GetFiltersListQuery(), cancellationToken);

            return Ok(dto);
        }
        
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetFilterById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var dto = await _mediator.Send(new GetFilterByIdQuery(id), cancellationToken);

            return Ok(dto);
        }
        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateFilter([FromBody] CreateFilterDto createFilterDto,
            CancellationToken cancellationToken)
        {
            var dto = await _mediator.Send(new CreateFilterCommand(createFilterDto), cancellationToken);

            return CreatedAtAction(nameof(GetFilterById), new { dto.Id }, dto);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateFilter([FromRoute] Guid id, [FromBody] UpdateFilterDto updateFilterDto,
            CancellationToken cancellationToken)
        {
            var dto = await _mediator.Send(new UpdateFilterCommand(id, updateFilterDto), cancellationToken);

            return CreatedAtAction(nameof(GetFilterById), new { dto.Id }, dto);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteFilter([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteFilterCommand(id), cancellationToken);

            return NoContent();
        }
    }
}
