using HackathonBackend.Application.Common.Interfaces.Persistance;
using HackathonBackend.Domain.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HackathonBackend.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly BackendDbContext _dbContext;

    public UnitOfWork(BackendDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateTimestamps();

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private void UpdateTimestamps()
    {
        IEnumerable<EntityEntry<IHasTimestamps>> entries = _dbContext
            .ChangeTracker
            .Entries<IHasTimestamps>();

        foreach (EntityEntry<IHasTimestamps> entityEntry in entries)
        {
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Property(x => x.CreatedDateTime)
                    .CurrentValue = DateTime.UtcNow;
            }

            if (entityEntry.State == EntityState.Modified)
            {
                entityEntry.Property(x => x.UpdatedDateTime)
                    .CurrentValue = DateTime.UtcNow;
            }
        }

    }
}