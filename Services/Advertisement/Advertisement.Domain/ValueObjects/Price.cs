using Advertisement.Domain.Enums;

namespace Advertisement.Domain.ValueObjects;

public class Price : ValueObject
{
    public double Value { get; set; }
    public Currency Currency { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
        yield return Currency;
    }
}
