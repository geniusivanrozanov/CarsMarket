using Chat.Application.Interfaces.Services;
using Chat.Application.QueryParameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat.WebAPI.Controllers;

[Route("api/chats/{chatId}/[controller]")]
[ApiController]
public class MessagesController : ControllerBase
{
    private readonly IMessageService _messageService;

    public MessagesController(IMessageService messageService)
    {
        _messageService = messageService;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetMessages([FromRoute] Guid chatId,
        [FromQuery] MessageQueryParameters queryParameters,
        CancellationToken cancellationToken)
    {
        var dto = await _messageService.GetMessagesByChatIdAsync(chatId, queryParameters, cancellationToken);

        return Ok(dto);
    }
}
