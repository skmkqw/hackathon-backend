using HackathonBackend.Domain.Common.Models;
using HackathonBackend.Domain.UserAggregate.ValueObjects;

namespace HackathonBackend.Domain.UserAggregate;

public sealed class User : AppUser<UserId>
{
    private User(
        UserId id, 
        string firstName, 
        string lastName, 
        string email, 
        string password) : base(id, firstName, lastName, email, password)
    {
    }

    public static User Create( 
        string firstName,
        string lastName, 
        string email,
        string password)
    {
        return new User(
            UserId.CreateUnique(), 
            firstName, lastName,
            email, 
            password);
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