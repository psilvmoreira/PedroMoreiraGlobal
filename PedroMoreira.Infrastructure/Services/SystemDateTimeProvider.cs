using PedroMoreira.Application.Common.Services;

namespace PedroMoreira.Infrastructure.Services
{
    internal sealed class SystemDateTimeProvider : IDateTImeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
