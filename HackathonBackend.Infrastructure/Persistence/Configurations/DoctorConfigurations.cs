using HackathonBackend.Domain.DoctorAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HackathonBackend.Infrastructure.Persistence.Configurations;


public class DoctorConfigurations : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        DoctorsTable(builder);
    }

    private void DoctorsTable(EntityTypeBuilder<Doctor> builder)
    {
        builder.ToTable("Doctors");
        
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
        
        //Specialization
        builder.Property(x => x.Specialization)
            .IsRequired()
            .HasMaxLength(100);
        
        //Experience
        builder.Property(x => x.Experience)
            .IsRequired();
        
        //Rating
        builder.Property(x => x.Rating)
            .IsRequired();

        builder.OwnsOne(x => x.Clinic);
        
        //PhoneNUmber
        builder.Property(x => x.PhoneNumber)
            .IsRequired()
            .HasMaxLength(12);
    }
}