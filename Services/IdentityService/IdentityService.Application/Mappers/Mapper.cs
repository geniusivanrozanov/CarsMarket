using IdentityService.Application.DTOs;
using IdentityService.Application.Interfaces;
using IdentityService.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace IdentityService.Application.Mappers;

[Mapper]
public partial class Mapper : IMapper
{
    public partial UserEntity ToUserEntity(RegisterDto registerDto);

    public partial UserDto ToUserDto(UserEntity userEntity);
}