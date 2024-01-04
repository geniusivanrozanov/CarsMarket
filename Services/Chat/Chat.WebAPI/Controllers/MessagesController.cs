using Chat.Application.Interfaces.Services;
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
    public async Task<IActionResult> GetMessages(Guid chatId, CancellationToken cancellationToken)
    {
        var dto = await _messageService.GetMessagesByChatIdAsync(chatId, cancellationToken);

        return Ok(dto);
    }
}
