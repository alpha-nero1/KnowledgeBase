using ProperCleanArchitecture.Application.Shared.Interfaces;

namespace ProperCleanArchitecture.Application.Shared.Services;

public class DateService : IDateService
{
    public DateTime UtcNow => DateTime.UtcNow;
}