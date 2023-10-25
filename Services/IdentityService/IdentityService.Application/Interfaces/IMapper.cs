using IdentityService.Application.DTOs;
using IdentityService.Domain.Entities;

namespace IdentityService.Application.Interfaces;

public interface IMapper
{
    IQueryable<UserDto> ProjectToUserDto(IQueryable<UserEntity> q);
    
    UserEntity ToUserEntity(RegisterDto registerDto);
    UserDto ToUserDto(UserEntity userEntity);
}