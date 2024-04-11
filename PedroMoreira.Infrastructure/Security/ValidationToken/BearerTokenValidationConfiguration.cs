using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PedroMoreira.Infrastructure.Settings.Token;
using System.Text;


namespace PedroMoreira.Infrastructure.Security.ValidationToken
{
    internal sealed class BearerTokenValidationConfiguration(IOptions<TokenSettings> Settings) 
        : IConfigureNamedOptions<JwtBearerOptions>
    {
        private readonly TokenSettings _settings = Settings.Value;

        public void Configure(string? name, JwtBearerOptions options) => Configure(options);

        public void Configure(JwtBearerOptions options)
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = _settings.Issuer,
                ValidAudience = _settings.Audience,
                
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_settings.Secret))
            };
        }
    }
}
