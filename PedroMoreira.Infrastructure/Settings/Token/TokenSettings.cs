using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Runtime.CompilerServices;

namespace PedroMoreira.Infrastructure.Settings.Token
{
    internal sealed class TokenSettings
    {
        public const string Name = nameof(TokenSettings);

        public string Audience { get; init; }
        public string Issuer { get; init; }
        public bool HasExpirationTime { get; init; }
        public int ExpirationTime { get; init; }
        public string Secret { get; init; }
        public int RefreshTokenExpire { get; init; }
    }
}
