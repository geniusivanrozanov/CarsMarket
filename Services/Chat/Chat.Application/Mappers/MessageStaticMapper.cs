using Chat.Application.DTOs.Message;
using Chat.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Chat.Application.Mappers;

[Mapper]
public static partial class MessageStaticMapper
{
    public static partial GetMessageDto ToGetMessageDto(this MessageEntity messageEntity);
    
    public static partial MessageEntity ToMessageEntity(this SendMessageDto sendMessageDto);
    
    public static partial IQueryable<GetMessageDto> ProjectToGetMessageDto(this IQueryable<MessageEntity> queryable);
    
    public static IQueryable<TResult> ProjectTo<TResult>(this IQueryable<MessageEntity> queryable)
    {
        if (queryable is IQueryable<TResult> query) return query;

        return queryable.MapTo<IQueryable<TResult>>();
    }

    private static partial TResult MapTo<TResult>(this object source);
}
