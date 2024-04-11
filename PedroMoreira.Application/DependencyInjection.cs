using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Microsoft.Extensions.Configuration;
using PedroMoreira.Application.Common.Settings;

namespace PedroMoreira.Application
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly))
                .AddSettings(configuration);

            return services;
        }

        private static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddOptions<ValidationSettings>(
                    configuration.GetSection(ValidationSettings.Name).Value);


            return services;
        }

    }
}
