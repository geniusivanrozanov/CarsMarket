using Chat.Application.DTOs.Chat;
using Chat.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chat.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChatController : ControllerBase
{
    private readonly IChatService _chatService;

    public ChatController(IChatService chatService)
    {
        _chatService = chatService;
    }
    
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetChats(CancellationToken cancellationToken)
    {
        var dto = await _chatService.GetChatsAsync(cancellationToken);

        return Ok(dto);
    }
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateChat([FromBody] CreateChatDto createChatDto, CancellationToken cancellationToken)
    {
        var dto = await _chatService.CreateChatAsync(createChatDto, cancellationToken);

        return Ok(dto);
    }
}