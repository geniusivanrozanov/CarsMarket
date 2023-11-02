using FluentValidation;
using IdentityService.Application.DTOs;

namespace IdentityService.Application.Validators;

public class RefreshTokenDtoValidator : AbstractValidator<RefreshTokenDto>
{
    private const int RefreshTokenLength = 32;

    public RefreshTokenDtoValidator()
    {
        RuleFor(dto => dto.RefreshToken)
            .NotNull()
            .NotEmpty()
            .Length(RefreshTokenLength);
    }
}
