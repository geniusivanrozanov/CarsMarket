using Chat.Application.DTOs.Chat;
using Chat.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Chat.Application.Mappers;

[Mapper]
public static partial class ChatStaticMapper
{
    public static partial GetChatDto ToGetChatDto(this ChatEntity chatEntity);

    public static partial ChatEntity ToChatEntity(this CreateChatDto createChatDto);

    public static partial IQueryable<GetChatDto> ProjectToGetChatDto(this IQueryable<ChatEntity> queryable);


    public static IQueryable<TResult> ProjectTo<TResult>(this IQueryable<ChatEntity> queryable)
    {
        if (queryable is IQueryable<TResult> query)
        {
            return query;
        }

        return queryable.MapTo<IQueryable<TResult>>();
    }

    private static partial TResult MapTo<TResult>(this object source);
}
