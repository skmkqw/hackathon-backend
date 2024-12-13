using HackathonBackend.Domain.Common.Models;

namespace HackathonBackend.Domain.DoctorAggregate.ValueObjects;

public class Clinic : ValueObject
{
    public string Name { get; private set; }

    public string Address { get; private set; }

    public float Rating { get; private set; }

    private Clinic(string name, string address, float rating)
    {
        Name = name;
        Address = address;
        Rating = rating;
    }

    public static Clinic Create(string name, string address, float rating)
    {
        return new Clinic(name, address, rating);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Address;
        yield return Rating;
    }
}