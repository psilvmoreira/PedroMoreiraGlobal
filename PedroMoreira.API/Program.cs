using PedroMoreira.API.Extensions;
using PedroMoreira.Application;
using PedroMoreira.Domain;
using PedroMoreira.Infrastructure;

namespace PedroMoreira.API
{
    public class Program
    {
        public static void Main(string[] args) 
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services
                .AddPresentation()
                .AddInfrastructure(builder.Configuration)
                .AddDomain(builder.Configuration)
                .AddApplication(builder.Configuration);

            var app = builder.Build();

            app.UseExceptionHandler("/error");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.ApplyMigrations();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}