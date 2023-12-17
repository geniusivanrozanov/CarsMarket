using CarsCatalog.Application.DTOs;
using FluentValidation;

namespace CarsCatalog.Application.Validators.Model;

public class UpdateModelDtoValidator : AbstractValidator<UpdateModelDto>
{
    public UpdateModelDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty();
    }
}
