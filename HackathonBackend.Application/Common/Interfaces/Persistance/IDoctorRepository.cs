using HackathonBackend.Domain.DoctorAggregate;
using HackathonBackend.Domain.DoctorAggregate.ValueObjects;

namespace HackathonBackend.Application.Common.Interfaces.Persistance;

public interface IDoctorRepository
{
    Task<Doctor?> FindByIdAsync(DoctorId id);
    
    Task<Doctor?> FindByEmailAsync(string email);
    
    void Add(Doctor user);
}