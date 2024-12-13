namespace HackathonBackend.Domain.Common.Models;

public abstract class AppUser<TId> : AggregateRoot<TId> where TId : notnull 
{
    public string FirstName { get; protected set;}

    public string LastName { get; protected set;}

    public string Email { get; }

    public string Password { get; }

    protected AppUser(TId id, string firstName, string lastName, string email, string password) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
    }
    
#pragma warning disable CS8618
    protected AppUser()
#pragma warning restore CS8618
    {
    }
}