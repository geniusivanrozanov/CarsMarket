using FluentValidation;
using IdentityService.Application.Extensions;
using IdentityService.Application.QueryParameters;

namespace IdentityService.Application.Validators;

public class UserQueryParametersValidator : AbstractValidator<UserQueryParameters>
{
    public UserQueryParametersValidator()
    {
        When(parameters => parameters.OrderBy is not null, () =>
        {
            RuleFor(parameters => parameters.OrderBy!)
                .EntryOf(new List<string>
                {
                    "Email",
                    "FirstName",
                    "LastName"
                });
        });
    }
}