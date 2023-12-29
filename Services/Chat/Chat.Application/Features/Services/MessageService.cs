using Chat.Application.DTOs.Message;
using Chat.Application.Exceptions;
using Chat.Application.Interfaces.Repositories;
using Chat.Application.Interfaces.Services;
using Chat.Application.Mappers;
using Microsoft.Extensions.Logging;

namespace Chat.Application.Features.Services;

public class MessageService : IMessageService
{
    private readonly IMessageRepository _messageRepository;
    private readonly IChatRepository _chatRepository;
    private readonly ICurrentUser _currentUser;
    private readonly TimeProvider _timeProvider;
    private readonly ILogger<MessageService> _logger;

    public MessageService(IMessageRepository messageRepository, IChatRepository chatRepository, ILogger<MessageService> logger, ICurrentUser currentUser, TimeProvider timeProvider)
    {
        _messageRepository = messageRepository;
        _chatRepository = chatRepository;
        _logger = logger;
        _currentUser = currentUser;
        _timeProvider = timeProvider;
    }

    public async Task<IEnumerable<GetMessageDto>> GetMessagesByChatIdAsync(Guid chatId, CancellationToken cancellationToken = default)
    {
        await CheckChatAndMemberCompatibility(chatId, _currentUser.Id, cancellationToken);

        var messagesDto = await _messageRepository.GetMessagesByChatIdAsync<GetMessageDto>(chatId, cancellationToken);

        return messagesDto;
    }

    public async Task<GetMessageDto> SendMessageAsync(SendMessageDto sendMessageDto, CancellationToken cancellationToken = default)
    {
        await CheckChatAndMemberCompatibility(sendMessageDto.ChatId, _currentUser.Id, cancellationToken);

        var currentTime = _timeProvider.GetUtcNow();
        
        var messageEntity = sendMessageDto.ToMessageEntity();
        messageEntity.SenderId = _currentUser.Id;
        messageEntity.CreatedAt = currentTime;

        await _messageRepository.CreateMessageAsync(messageEntity, cancellationToken);
        await _chatRepository.UpdateLastMessageAsync(messageEntity, cancellationToken);

        var messageDto = messageEntity.ToGetMessageDto();

        return messageDto;
    }
    
    private async Task CheckChatAndMemberCompatibility(Guid chatId, Guid memberId, CancellationToken cancellationToken)
    {
        if (!await _chatRepository.ExistsWithIdAsync(chatId, cancellationToken))
        {
            _logger.LogInformation("Chat with id '{ChatId}' doesn't exist", chatId);
            throw new NotExistsException($"Chat with id '{chatId}' doesn't exist");
        }

        if (!await _chatRepository.ExistsWithIdAndMemberIdAsync(chatId, memberId, cancellationToken))
        {
            _logger.LogInformation("User with id '{UserId}' is not a member of chat with id '{ChatId}'", memberId, chatId);
            throw new ForbiddenActionException($"User with id '{memberId}' is not a member of chat with id '{chatId}'");
        }
    }
}
