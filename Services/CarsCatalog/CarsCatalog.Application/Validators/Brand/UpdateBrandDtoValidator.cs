using CarsCatalog.Application.DTOs;
using FluentValidation;

namespace CarsCatalog.Application.Validators.Brand;

public class UpdateBrandDtoValidator : AbstractValidator<UpdateBrandDto>
{
    public UpdateBrandDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty();
    }
}
