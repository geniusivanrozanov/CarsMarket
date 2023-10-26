using FluentValidation;
using IdentityService.Application.DTOs;

namespace IdentityService.Application.Validators;

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(dto => dto.Email)
            .NotNull()
            .NotEmpty()
            .EmailAddress();

        RuleFor(dto => dto.Password)
            .NotNull()
            .NotEmpty();
    }
}