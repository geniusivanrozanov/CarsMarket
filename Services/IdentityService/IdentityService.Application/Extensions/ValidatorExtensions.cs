using FluentValidation;

namespace IdentityService.Application.Extensions;

public static class ValidatorExtensions
{
    public static IRuleBuilderOptions<T, string> StartsWithCapital<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .Must(s => char.IsUpper(s[0]))
            .WithMessage("Must start with a capital letter");
    }

    public static IRuleBuilderOptions<T, string> ConsistsOfLetters<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .Must(s => s.ToCharArray().All(char.IsLetter))
            .WithMessage("Must consist of letters only");
    }

    public static IRuleBuilderOptions<T, string> EntryOf<T>(this IRuleBuilder<T, string> ruleBuilder,
        List<string> acceptableValues)
    {
        return ruleBuilder
            .Must(s => acceptableValues.Contains(s, StringComparer.InvariantCultureIgnoreCase))
            .WithMessage($"Must be one of: {string.Join(", ", acceptableValues)}");
    }
}
