using HackathonBackend.Application.Common.Interfaces.Persistance;
using HackathonBackend.Domain.DoctorAggregate;
using HackathonBackend.Domain.DoctorAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace HackathonBackend.Infrastructure.Persistence.Repositories;

public class DoctorRepository : IDoctorRepository
{
    private readonly BackendDbContext _dbContext;

    public DoctorRepository(BackendDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Doctor?> FindByIdAsync(DoctorId id)
    {
        return await _dbContext.Doctors.FindAsync(id);
    }

    public async Task<Doctor?> FindByEmailAsync(string email)
    {
        return await _dbContext.Doctors.FirstOrDefaultAsync(u => u.Email == email);
    }

    public void Add(Doctor doctor)
    {
        _dbContext.Doctors.Add(doctor);
    }
}