using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace PedroMoreira.Domain
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddDomain(this IServiceCollection service, IConfiguration config)
        {
            return service;
        }
    }
}
