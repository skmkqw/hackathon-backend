using HackathonBackend.Domain.Common.Models;

namespace HackathonBackend.Domain.DoctorAggregate.ValueObjects;

public class Clinic : ValueObject
{
    public string Name { get; set; }

    public string Address { get; set; }

    public float Rating { get; set; }

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
        throw new NotImplementedException();
    }
}