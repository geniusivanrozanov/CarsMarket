using Chat.Application.DTOs.Message;
using Chat.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Chat.Application.Mappers;

[Mapper]
public static partial class MessageStaticMapper
{
    public static partial GetMessageDto ToGetMessageDto(this MessageEntity messageEntity);
    
    public static partial MessageEntity ToMessageEntity(this SendMessageDto sendMessageDto);
}
