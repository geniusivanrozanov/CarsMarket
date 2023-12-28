using Chat.Application.DTOs.Chat;
using Chat.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Chat.Application.Mappers;

[Mapper]
[UseStaticMapper(typeof(MemberStaticMapper))]
public static partial class ChatStaticMapper
{
    public static partial GetChatDto ToGetChatDto(this ChatEntity chatEntity);

    public static partial ChatEntity ToChatEntity(this CreateChatDto createChatDto);
}
