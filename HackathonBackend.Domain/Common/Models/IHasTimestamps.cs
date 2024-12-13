namespace HackathonBackend.Domain.Common.Models;

public interface IHasTimestamps
{
    DateTime CreatedDateTime { get; set; }
    
    DateTime UpdatedDateTime { get; set; }
}