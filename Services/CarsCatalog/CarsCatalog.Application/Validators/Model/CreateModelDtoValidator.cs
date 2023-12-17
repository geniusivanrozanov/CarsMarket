using CarsCatalog.Application.DTOs;
using FluentValidation;

namespace CarsCatalog.Application.Validators.Model;

public class CreateModelDtoValidator : AbstractValidator<CreateModelDto>
{
    public CreateModelDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.BrandId)
            .NotEmpty();
    }
}
