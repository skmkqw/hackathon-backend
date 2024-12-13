using HackathonBackend.Domain.Common.Attributes;
using HackathonBackend.Domain.Common.Models;
using HackathonBackend.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HackathonBackend.Domain.DoctorAggregate.ValueObjects;


[EfCoreValueConverter(typeof(UserId.UserIdValueConverter))]
public class DoctorId : ValueObject, IEntityId<DoctorId, Guid>
{
    public Guid Value { get; }

    private DoctorId(Guid value)
    {
        Value = value;
    }
    
    public static DoctorId CreateUnique()
    {
        return new DoctorId(Guid.NewGuid());
    }

    public static DoctorId Create(Guid value)
    {
        return new DoctorId(value);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    
    public class DoctorIdValueConverter : ValueConverter<DoctorId, Guid>
    {
        public DoctorIdValueConverter()
            : base(
                id => id.Value,
                value => Create(value)
            ) { }
    }
}