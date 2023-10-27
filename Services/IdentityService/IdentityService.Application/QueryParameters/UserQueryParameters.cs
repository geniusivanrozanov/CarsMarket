using System.Linq.Expressions;
using IdentityService.Domain.Entities;

namespace IdentityService.Application.QueryParameters;

public record UserQueryParameters : QueryParametersBase<UserEntity>
{
    public string? Email { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }

    public override Expression<Func<UserEntity, object>> GetOrderByExpression()
    {
        if (OrderBy is null) throw new ArgumentNullException();

        return OrderBy.ToLower() switch
        {
            "email" => entity => entity.Email!,
            "firstname" => entity => entity.FirstName,
            "lastname" => entity => entity.LastName,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public override IEnumerable<Expression<Func<UserEntity, bool>>> GetFilterExpressions()
    {
        if (FirstName is not null)
            yield return u => u.FirstName.ToLower().Contains(FirstName.ToLower());

        if (LastName is not null)
            yield return u => u.LastName.ToLower().Contains(LastName.ToLower());

        if (Email is not null)
            yield return u => u.Email!.ToLower().Contains(Email.ToLower());
    }
}