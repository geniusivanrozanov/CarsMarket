using FluentValidation;
using IdentityService.Application.DTOs;
using IdentityService.Application.Extensions;

namespace IdentityService.Application.Validators;

public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    private const int FirstNameMaxLength = 64;
    private const int LastNameMaxLength = 64;

    public RegisterDtoValidator()
    {
        RuleFor(dto => dto.Email)
            .NotNull()
            .NotEmpty()
            .EmailAddress();

        RuleFor(dto => dto.Password)
            .NotNull()
            .NotEmpty();

        RuleFor(dto => dto.FirstName)
            .NotNull()
            .NotEmpty()
            .MaximumLength(FirstNameMaxLength)
            .StartsWithCapital()
            .ConsistsOfLetters();

        RuleFor(dto => dto.LastName)
            .NotNull()
            .NotEmpty()
            .MaximumLength(LastNameMaxLength)
            .StartsWithCapital()
            .ConsistsOfLetters();
    }
}