using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.OpenApi.Models;
using PedroMoreira.API.Common.Errors;

namespace PedroMoreira.API
{
    public static class DependencieInjection
    {

        public static IServiceCollection AddPresentation(this IServiceCollection service)
        {
            service.AddSingleton<ProblemDetailsFactory, ErrorDetailsFactory>();

            service.AddControllers();
            service.AddProblemDetails();
            service.AddSwagger();


            return service;
        }


        public static IServiceCollection AddSwagger(this IServiceCollection service)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            service.AddEndpointsApiExplorer();
            service.AddSwaggerGen(o =>
            {
                o.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "PedroMoreira API"
                });

                o.AddSecurityDefinition(
                    JwtBearerDefaults.AuthenticationScheme,
                    new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = JwtBearerDefaults.AuthenticationScheme,
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description = "JWT Authorization header using the Bearer scheme. \\r\\n\\r\\n Enter 'Bearer' [space] and then your token in the text input below.\\r\\n\\r\\nExample: \\\"Bearer 12345abcdef\\\"\""
                    }
                );

                o.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        },

                        new string[] {}
                    }
                });
            });


            return service;
        }
    }
}
