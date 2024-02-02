using Identity.Messages.Contracts;
using IdentityService.Application.DTOs;
using IdentityService.Domain.Entities;

namespace IdentityService.Application.Interfaces;

public interface IMapper
{
    IQueryable<UserDto> ProjectToUserDto(IQueryable<UserEntity> q);

    UserEntity ToUserEntity(RegisterDto registerDto);
    UserDto ToUserDto(UserEntity userEntity);
    UserUpdatedMessage ToUserUpdatedMessage(UserEntity userEntity);
    void ToUserEntity(UpdateUserDto updateUserDto, UserEntity userEntity);
}
