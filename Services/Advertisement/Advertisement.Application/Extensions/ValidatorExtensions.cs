using System.Text.RegularExpressions;
using FluentValidation;

namespace Advertisement.Application.Extensions;

public static class ValidatorExtensions
{
    private static readonly Regex VinRegex = new Regex("[A-HJ-NPR-Z0-9]{13}[0-9]{4}", RegexOptions.Compiled);
    
    public static IRuleBuilderOptions<T, string> IsValidVinNumber<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .Must(x => VinRegex.IsMatch(x))
            .WithMessage("Invalid VIN Format");
    }
    
    public static IRuleBuilderOptions<T, string> EntryOf<T>(this IRuleBuilder<T, string> ruleBuilder,
        List<string> acceptableValues)
    {
        return ruleBuilder
            .Must(s => acceptableValues.Contains(s, StringComparer.InvariantCultureIgnoreCase))
            .WithMessage($"Must be one of: {string.Join(", ", acceptableValues)}");
    }
}
