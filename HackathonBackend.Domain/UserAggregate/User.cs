using HackathonBackend.Domain.Common.Models;
using HackathonBackend.Domain.UserAggregate.ValueObjects;

namespace HackathonBackend.Domain.UserAggregate;

public sealed class User : AppUser<UserId>
{
    public DateOnly BirthDate { get; private set; }
    
    private User(
        UserId id, 
        string firstName, 
        string lastName, 
        string email, 
        string password,
        string phoneNumber,
        DateOnly birthDate) : base(id, firstName, lastName, email, password, phoneNumber)
    {
        BirthDate = birthDate;
    }

    public static User Create( 
        string firstName,
        string lastName, 
        string email,
        string password,
        string phoneNumber,
        DateOnly birthDate)
    {
        return new User(
            UserId.CreateUnique(), 
            firstName, lastName,
            email, 
            password,
            phoneNumber,
            birthDate);
    }

    public void Update(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

#pragma warning disable CS8618
    private User()
#pragma warning restore CS8618
    {
    }
}