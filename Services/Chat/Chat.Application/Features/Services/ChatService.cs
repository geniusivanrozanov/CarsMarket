using Advertisement.gRPC.Contracts;
using Advertisement.gRPC.Contracts.Replies;
using Advertisement.gRPC.Contracts.Requests;
using Chat.Application.DTOs.Chat;
using Chat.Application.Exceptions;
using Chat.Application.Interfaces.Repositories;
using Chat.Application.Interfaces.Services;
using Chat.Application.Mappers;
using Chat.Domain.Entities;
using Identity.gRPC.Contracts;
using Identity.gRPC.Contracts.Enums;
using Identity.gRPC.Contracts.Requests;
using Microsoft.Extensions.Logging;
using InvalidOperationException = Chat.Application.Exceptions.InvalidOperationException;

namespace Chat.Application.Features.Services;

public class ChatService : IChatService
{
    private readonly IChatRepository _chatRepository;
    private readonly ICurrentUser _currentUser;
    private readonly IAdvertisementService _advertisementService;
    private readonly IIdentityService _identityService;
    private readonly ILogger<ChatService> _logger;

    public ChatService(
        IChatRepository chatRepository,
        ICurrentUser currentUser,
        IAdvertisementService advertisementService,
        IIdentityService identityService,
        ILogger<ChatService> logger)
    {
        _chatRepository = chatRepository;
        _currentUser = currentUser;
        _advertisementService = advertisementService;
        _identityService = identityService;
        _logger = logger;
    }

    public async Task<IEnumerable<GetChatDto>> GetChatsAsync(CancellationToken cancellationToken = default)
    {
        var currentUserId = _currentUser.Id;
        var chatsDto = await _chatRepository.GetChatsByMemberIdAsync<GetChatDto>(currentUserId, cancellationToken);

        return chatsDto;
    }

    public async Task<GetChatDto> CreateChatAsync(CreateChatDto createChatDto,
        CancellationToken cancellationToken = default)
    {
        var adInfoReply = await _advertisementService.GetAdInfoByIdAsync(new GetAdInfoByIdRequest
        {
            AdId = createChatDto.AdId
        });

        if (adInfoReply.Error is Advertisement.gRPC.Contracts.Enums.Error.AdNotFound)
        {
            _logger.LogInformation("gRPC call failed with message '{Message}'", adInfoReply.ErrorMessage);
            throw new NotExistsException(adInfoReply.ErrorMessage!);
        }

        if (adInfoReply.OwnerId == _currentUser.Id)
        {
            _logger.LogInformation("User with id '{UserId}' tried to create chat with himself", _currentUser.Id);
            throw new InvalidOperationException("User cannot create chat with himself");
        }

        var members = await GetMembersAsync(adInfoReply.OwnerId, _currentUser.Id);

        var chatEntity = createChatDto.ToChatEntity();
        chatEntity.Members = members;

        await _chatRepository.CreateChatAsync(chatEntity, cancellationToken);

        var chatDto = chatEntity.ToGetChatDto();

        return chatDto;
    }

    private async Task<ICollection<MemberEntity>> GetMembersAsync(Guid ownerId, Guid buyerId)
    {
        var getOwnerNameRequest = new GetUserFirstNameByIdRequest
        {
            UserId = ownerId
        };

        var getBuyerRequest = new GetUserFirstNameByIdRequest
        {
            UserId = buyerId
        };

        var membersNamesReplies = await Task.WhenAll(
            _identityService.GetUserFirstNameAsync(getOwnerNameRequest),
            _identityService.GetUserFirstNameAsync(getBuyerRequest));

        foreach (var reply in membersNamesReplies)
            if (reply.Error is Error.UserNotFound)
            {
                _logger.LogInformation("gRPC call failed with message '{Message}'", reply.ErrorMessage);
                throw new NotExistsException(reply.ErrorMessage!);
            }

        var owner = new MemberEntity
        {
            Id = ownerId,
            Name = membersNamesReplies[0].FirstName
        };

        var buyer = new MemberEntity
        {
            Id = buyerId,
            Name = membersNamesReplies[1].FirstName
        };

        var members = new[] { owner, buyer };
        return members;
    }
}
