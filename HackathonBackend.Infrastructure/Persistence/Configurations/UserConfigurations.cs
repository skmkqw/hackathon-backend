using HackathonBackend.Domain.UserAggregate;

namespace HackathonBackend.Infrastructure.Persistence.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        ConfigureUsersTable(builder);
    }

    private void ConfigureUsersTable(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        
        //PK
        builder.HasKey(x => x.Id);
        
        //ID
        builder.Property(x => x.Id)
            .ValueGeneratedNever();
        
        //FirstName
        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(100);
        
        //LastName
        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(200);
        
        //Email
        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(100);
        
        //Password
        builder.Property(x => x.Password)
            .IsRequired()
            .HasMaxLength(100);
        
        //PhoneNumber
        builder.Property(x => x.BirthDate)
            .IsRequired();
        
        //PhoneNumber
        builder.Property(x => x.PhoneNumber)
            .IsRequired()
            .HasMaxLength(12);
    }
}