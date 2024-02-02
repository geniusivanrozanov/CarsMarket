using FavoriteFilters.Application.DTOs.Filter;
using FluentValidation;

namespace FavoriteFilters.Application.Validators;

public class CreateFilterValidator : AbstractValidator<CreateFilterDto>
{
    public CreateFilterValidator()
    {
        RuleFor(x => x.CronMinute)
            .InclusiveBetween(0, 59);
        
        RuleFor(x => x.CronHour)
            .InclusiveBetween(0, 23);
        
        RuleFor(x => x.CronDayOfMonth)
            .InclusiveBetween(1, 31);
        
        RuleFor(x => x.CronMonth)
            .InclusiveBetween(1, 12);
        
        RuleFor(x => x.CronDayOfWeek)
            .InclusiveBetween(0, 6);
    }
}
