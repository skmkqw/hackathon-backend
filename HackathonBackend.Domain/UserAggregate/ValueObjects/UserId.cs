using HackathonBackend.Domain.Common.Attributes;
using HackathonBackend.Domain.Common.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HackathonBackend.Domain.UserAggregate.ValueObjects;

[EfCoreValueConverter(typeof(UserIdValueConverter))]
public class UserId : ValueObject, IEntityId<UserId, Guid>
{
    public Guid Value { get; }

    private UserId(Guid value)
    {
        Value = value;
    }
    
    public static UserId CreateUnique()
    {
        return new UserId(Guid.NewGuid());
    }

    public static UserId Create(Guid value)
    {
        return new UserId(value);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    
    public class UserIdValueConverter : ValueConverter<UserId, Guid>
    {
        public UserIdValueConverter()
            : base(
                id => id.Value,
                value => Create(value)
            ) { }
    }
}