using Advertisement.Application.DTOs.Ad;
using Advertisement.Application.Extensions;
using FluentValidation;

namespace Advertisement.Application.Validators.Ad;

public class CreateAdDtoValidator : AbstractValidator<CreateAdDto>
{
    public CreateAdDtoValidator(TimeProvider timeProvider)
    {
        RuleFor(x => x.Currency)
            .IsInEnum();

        RuleFor(x => x.Vin!)
            .IsValidVinNumber()
            .When(x => x.Vin is not null);

        RuleFor(x => x.Year)
            .InclusiveBetween(DateTimeOffset.MinValue.Year, timeProvider.GetUtcNow().Year);

        RuleFor(x => x.Mileage)
            .InclusiveBetween(0, 10_000_000);

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(100);
    }
}
