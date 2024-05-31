using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PedroMoreira.Infrastructure.Persistence;
using PedroMoreira.Infrastructure.Services;
using PedroMoreira.Infrastructure.Settings.Token;
using PedroMoreira.Infrastructure.Security.ValidationToken;
using PedroMoreira.Application.Common.Abstractions.Authentication;
using PedroMoreira.Application.Common.Abstractions.Data;
using PedroMoreira.Application.Common.Services;
using PedroMoreira.Infrastructure.Persistence.Repository;
using PedroMoreira.Application.Common.Repository;

namespace PedroMoreira.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services
                .AddSettings(config)
                .AddServices()
                .AddPersistence(config)
                .AddAuthentication(config)
                .AddAuthorization();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IDateTImeProvider, SystemDateTimeProvider>();
            services.AddSingleton<IAuthTokenGeneratorService, AuthTokenGeneratorService>();

            return services;
        }

        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration manager)
        {
            services.AddDbContext<PostgresContext>(o => o.UseNpgsql(manager.GetConnectionString("PmDatabase")));
            services.AddScoped<IDbContext, PostgresContext>();
            services.AddScoped<IUnitofWork, PostgresContext>();

            // Repositories goes here
            services.AddScoped<IMemberRepository, MemberRepository>();


            return services;
        }

        public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration config) 
        {
            services
                .AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddBearerToken();

            return services;
        }

        public static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration Config)
        {
            services
                .AddOptions<TokenSettings>(
                    Config.GetSection(TokenSettings.Name).Value);

            services
                .ConfigureOptions<BearerTokenValidationConfiguration>();

            return services;
        }
    }
}
