using Chat.Application.DTOs.Message;
using Chat.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Chat.WebAPI.Hubs;

[Authorize]
public class MessageHub : Hub
{
    private readonly IChatService _chatService;
    private readonly IMessageService _messageService;

    public MessageHub(IChatService chatService, IMessageService messageService)
    {
        _chatService = chatService;
        _messageService = messageService;
    }
    
    public override async Task OnConnectedAsync()
    {
        var chats = await _chatService.GetChatsAsync();

        foreach (var chat in chats)
        {
            var groupName = GenerateGroupNameForChat(chat.Id);

            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }
        
        await base.OnConnectedAsync();
    }
    
    public async Task SendMessage(SendMessageDto sendMessageDto, CancellationToken cancellationToken)
    {
        var dto = await _messageService.SendMessageAsync(sendMessageDto, cancellationToken);

        var groupName = GenerateGroupNameForChat(dto.ChatId);

        await Clients.Groups(groupName).SendAsync("Receive message", dto, cancellationToken);
    }

    private string GenerateGroupNameForChat(Guid chatId)
    {
        return $"chat {chatId}";
    }
}
