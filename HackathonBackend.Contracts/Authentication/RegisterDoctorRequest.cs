namespace HackathonBackend.Contracts.Authentication;

public record RegisterDoctorRequest(    string FirstName,
    string LastName,
    string Email,
    string Password,
    string Specialization,
    float Rating,
    float Experience,
    string LicenseNumber,
    string PhoneNumber,
    CreateClinicRequest? Clinic
    );
    
public record CreateClinicRequest(string Name, string Address, float Rating);