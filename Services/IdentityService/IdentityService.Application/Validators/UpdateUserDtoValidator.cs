using FluentValidation;
using IdentityService.Application.DTOs;
using IdentityService.Application.Extensions;

namespace IdentityService.Application.Validators;

public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
{
    private const int FirstNameMaxLength = 64;
    private const int LastNameMaxLength = 64;
    
    public UpdateUserDtoValidator()
    {
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
