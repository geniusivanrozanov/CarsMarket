using Identity.Messages.Contracts;
using IdentityService.Application.DTOs;
using IdentityService.Application.Interfaces;
using IdentityService.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace IdentityService.Application.Mappers;

[Mapper]
public partial class Mapper : IMapper
{
    public partial IQueryable<UserDto> ProjectToUserDto(IQueryable<UserEntity> q);

    [MapProperty(nameof(RegisterDto.Email), nameof(UserEntity.UserName))]
    public partial UserEntity ToUserEntity(RegisterDto registerDto);

    public partial UserDto ToUserDto(UserEntity userEntity);
    
    [MapProperty(nameof(UserEntity.Id), nameof(UserUpdatedMessage.UserId))]
    [MapProperty(nameof(UserEntity.FirstName), nameof(UserUpdatedMessage.UpdatedFirstName))]
    [MapProperty(nameof(UserEntity.LastName), nameof(UserUpdatedMessage.UpdatedLastName))]
    public partial UserUpdatedMessage ToUserUpdatedMessage(UserEntity userEntity);

    public partial void ToUserEntity(UpdateUserDto updateUserDto, UserEntity userEntity);
}
