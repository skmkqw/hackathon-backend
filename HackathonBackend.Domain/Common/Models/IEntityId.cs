namespace HackathonBackend.Domain.Common.Models;

public interface IEntityId<T, TValue> where T : IEntityId<T, TValue>
{
    TValue Value { get; }

    static abstract T CreateUnique();
    
    static abstract T Create(TValue value);
}
