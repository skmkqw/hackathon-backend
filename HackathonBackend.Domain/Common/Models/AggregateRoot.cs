namespace HackathonBackend.Domain.Common.Models;

public abstract class AggregateRoot<TId> : Entity<TId>, IHasTimestamps where TId : notnull
{
    public DateTime CreatedDateTime { get; set; }
    
    public DateTime UpdatedDateTime { get; set; }
    
    protected AggregateRoot()
    {
    }
    
    protected AggregateRoot(TId id) : base(id)
    {
        CreatedDateTime = DateTime.UtcNow;
        UpdatedDateTime = DateTime.UtcNow;
    }
}