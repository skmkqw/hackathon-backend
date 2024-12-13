using HackathonBackend.Domain.Common.Models;
using HackathonBackend.Domain.DoctorAggregate.ValueObjects;

namespace HackathonBackend.Domain.DoctorAggregate;

public class Doctor : AppUser<DoctorId>
{
    public string Specialization { get; private  set; }

    public float Experience { get; private  set; }
    
    public float Rating { get; private  set; }

    public string LicenseNumber { get; private set; }
    
    public Clinic? Clinic { get; private  set; }

    private Doctor(
        DoctorId id, 
        string firstName, 
        string lastName, 
        string email, 
        string password,
        string specialization,
        float experience, 
        float rating,
        string licenseNumber,
        string phoneNumber) : base(id, firstName, lastName, email, password, phoneNumber)
    {
        Experience = experience;
        Specialization = specialization;
        Rating = rating;
        LicenseNumber = licenseNumber;
    }

    public static Doctor Create( 
        string firstName,
        string lastName, 
        string email,
        string password,
        string specialization,
        float experience, 
        float rating,
        string licenseNumber,
        string phoneNumber)
    {
        return new Doctor(
            DoctorId.CreateUnique(), 
            firstName, lastName,
            email, 
            password,
            specialization,
            experience,
            rating,
            licenseNumber,
            phoneNumber);
    }
    
    
    public void Update(string firstName, string lastName, string specialization, float experience, float rating)
    {
        FirstName = firstName;
        LastName = lastName;
        Specialization = specialization;
        Experience = experience;
        Rating = rating;
    }

    public void SetClinic(Clinic clinic)
    {
        Clinic = clinic;
    }

#pragma warning disable CS8618
    private Doctor()
#pragma warning restore CS8618
    {
    }
}