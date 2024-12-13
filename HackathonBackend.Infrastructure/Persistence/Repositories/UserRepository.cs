using HackathonBackend.Application.Common.Interfaces.Persistance;
using HackathonBackend.Domain.UserAggregate;
using HackathonBackend.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace HackathonBackend.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly BackendDbContext _dbContext;

    public UserRepository(BackendDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> FindByIdAsync(UserId id)
    {
        return await _dbContext.Users.FindAsync(id);
    }

    public async Task<User?> FindByEmailAsync(string email)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public void Add(User user)
    {
        _dbContext.Users.Add(user);
    }
}