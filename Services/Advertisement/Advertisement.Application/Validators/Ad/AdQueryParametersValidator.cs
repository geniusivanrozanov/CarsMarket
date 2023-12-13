using Advertisement.Application.Extensions;
using Advertisement.Application.QueryParameters;
using FluentValidation;

namespace Advertisement.Application.Validators.Ad;

public class AdQueryParametersValidator : AbstractValidator<AdQueryParameters>
{
    public AdQueryParametersValidator()
    {
        RuleFor(x => x.OrderBy!)
            .EntryOf(new List<string>
            {
                "Price",
                "Year",
                "Mileage"
            })
            .When(x => x.OrderBy is not null);

        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(0)
            .When(x => x.Page.HasValue);

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(0)
            .When(x => x.PageSize.HasValue);
    }
}
