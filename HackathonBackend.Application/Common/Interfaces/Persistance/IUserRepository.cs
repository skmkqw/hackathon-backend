using HackathonBackend.Domain.UserAggregate;
using HackathonBackend.Domain.UserAggregate.ValueObjects;

namespace HackathonBackend.Application.Common.Interfaces.Persistance;

public interface IUserRepository
{
    Task<User?> FindByIdAsync(UserId id);
    
    Task<User?> FindByEmailAsync(string email);
    
    void Add(User user);
}