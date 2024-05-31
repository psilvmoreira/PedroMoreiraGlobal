using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Microsoft.Extensions.Configuration;
using PedroMoreira.Application.Common.Settings;
using PedroMoreira.Application.Common.Behaviours;
using FluentValidation;
using System.Runtime.CompilerServices;
using System.Reflection;

namespace PedroMoreira.Application
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly))
                .AddFluentValidations()
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

        private static IServiceCollection AddFluentValidations(this IServiceCollection services)
        {
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehaviour<,>));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true);

            return services;
        }

    }
}
