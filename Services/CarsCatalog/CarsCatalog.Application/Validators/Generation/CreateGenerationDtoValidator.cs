using CarsCatalog.Application.DTOs;
using FluentValidation;

namespace CarsCatalog.Application.Validators.Generation;

public class CreateGenerationDtoValidator : AbstractValidator<CreateGenerationDto>
{
    public CreateGenerationDtoValidator(TimeProvider timeProvider)
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.ModelId)
            .NotEmpty();

        RuleFor(x => x.StartYear)
            .InclusiveBetween(DateTimeOffset.MinValue.Year, timeProvider.GetUtcNow().Year);

        RuleFor(x => new { x.StartYear, x.EndYear })
            .Must(arg => arg.EndYear >= arg.StartYear)
            .WithName(nameof(CreateGenerationDto.EndYear))
            .WithMessage($"Must be gather or equal to {nameof(CreateGenerationDto.StartYear)}")
            .When(x => x.EndYear.HasValue);
    }
}
