using HackathonBackend.Domain.Common.Attributes;
using HackathonBackend.Domain.DoctorAggregate;
using HackathonBackend.Domain.UserAggregate;
using HackathonBackend.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace HackathonBackend.Infrastructure.Persistence;

public class BackendDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    
    public DbSet<Doctor> Doctors { get; set; }

    
    public BackendDbContext(DbContextOptions<BackendDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BackendDbContext).Assembly);
        
        modelBuilder.AddStronglyTypedIdValueConverters<EfCoreValueConverterAttribute>();
    }
}