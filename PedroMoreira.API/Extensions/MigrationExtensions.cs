using Microsoft.EntityFrameworkCore;
using PedroMoreira.Infrastructure.Persistence;

namespace PedroMoreira.API.Extensions
{
    public static class MigrationExtensions
    {

        public static void ApplyMigrations(this IApplicationBuilder app) {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            using PostgresContext dbcontext = scope.ServiceProvider.GetRequiredService<PostgresContext>();
            //dbcontext.Database.Migrate();
        }
    }
}
