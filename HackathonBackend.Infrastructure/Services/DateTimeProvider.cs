using ZBank.Application.Common.Interfaces.Services;

namespace HackathonBackend.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}