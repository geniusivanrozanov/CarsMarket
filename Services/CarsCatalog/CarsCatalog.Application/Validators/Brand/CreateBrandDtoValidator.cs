using CarsCatalog.Application.DTOs;
using FluentValidation;

namespace CarsCatalog.Application.Validators.Brand;

public class CreateBrandDtoValidator : AbstractValidator<CreateBrandDto>
{
    public CreateBrandDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty();
    }
}
