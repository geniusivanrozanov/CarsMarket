using Chat.Application.DTOs.Member;
using Chat.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Chat.Application.Mappers;

[Mapper]
public static partial class  MemberStaticMapper
{
    public static partial GetMemberDto ToGetMemberDto(this MemberEntity memberEntity);
}
