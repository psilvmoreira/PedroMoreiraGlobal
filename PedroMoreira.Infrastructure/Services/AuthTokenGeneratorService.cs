using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PedroMoreira.Application.Common.Abstractions.Authentication;
using PedroMoreira.Application.Common.Services;
using PedroMoreira.Domain.Authentication.Contracts;
using PedroMoreira.Domain.Members.Entities;
using PedroMoreira.Infrastructure.Settings.Token;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PedroMoreira.Infrastructure.Services
{
    internal sealed class AuthTokenGeneratorService(
            IDateTImeProvider dataProvider,
            IOptions<TokenSettings> TokenSettings,
            IOptions<TokenValidationParameters> TokenParameters
        ) : IAuthTokenGeneratorService
    {
        private readonly IDateTImeProvider _DataProvider = dataProvider;
        private readonly TokenSettings _TokenSettings = TokenSettings.Value;
        private readonly TokenValidationParameters _Parameters = TokenParameters.Value;

        /// <summary>
        /// Get Token and Refresh Token for User with its expire datetime.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Returns <paramref name="TokenContract"/> with the Bearer token, Refresh token and expire date for both.  </returns>
        public async Task<AuthToken> GetAuthToken(Member user, List<string>? roles)
        {

            if (roles is null)
                throw new ArgumentException("Can´t create ");

            var descriptor = await GenTokenDescriptor(
                user, 
                roles);

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(descriptor);

            var refreshToken = await GetRefreshToken(
                descriptor.Expires!.Value);

            return new AuthToken(
                        JwtBearerDefaults.AuthenticationScheme,
                        tokenHandler.WriteToken(securityToken),
                        descriptor.Expires.Value,
                        refreshToken);
        }
        
        /// <summary>
        /// Validate and returns the ClaimsIdentity of old Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns>Returns the ClaimIdentity of old token</returns>
        public async Task<TokenValidationResult?> GetClaimsPrincipal(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            
            var validation = await tokenHandler.ValidateTokenAsync(token, _Parameters);

            return validation;
        }

        /// <summary>
        /// Check if the given token is valid
        /// </summary>
        /// <param name="user"></param>
        /// <param name="token"></param>
        /// <returns>Return <see langword="true"/> if valid and <see langword="False"/> if not.</returns>
        public async Task<bool> ValidateToken(Member? user, AuthToken? token)
        {

            if (user is null || token is null)
                return await Task.FromResult(false);

            if (token.RefreshToken.Expire <= _DataProvider.UtcNow 
                || token.RefreshToken.Expire <= _DataProvider.UtcNow)
                return await Task.FromResult(false);

            if (user.UserTokens
                    .Select(a => a.Value)
                    .Contains(token.RefreshToken.Token))
                return await Task.FromResult(false);

            return await Task.FromResult(true);
        }

        /// <summary>
        /// Get Token descriptor based on User Information.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="IssueAt"></param>
        /// <param name="Expire"></param>
        /// <returns></returns>
        private async Task<SecurityTokenDescriptor> GenTokenDescriptor(Member user, List<string> roles)
        {
            // Date time when token was generator 
            var IssueAt = _DataProvider.UtcNow;

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_TokenSettings.Secret));

            var claims = new List<Claim>
            {
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new (ClaimTypes.NameIdentifier, user.Id.ToString()), // User Id
                new (JwtRegisteredClaimNames.Name, user.Profile.FirstName), // User First Name
                new (JwtRegisteredClaimNames.FamilyName, user.Profile.LastName), // User Last Name
                new (JwtRegisteredClaimNames.UniqueName, user.UserName!), // Username
                new (JwtRegisteredClaimNames.Email, user.Email!), // User Email
                new (ClaimTypes.Role, string.Join(",", (roles ?? [] ))) // User Roles
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _TokenSettings.Issuer,
                IssuedAt = IssueAt,
                Subject = new ClaimsIdentity(claims),
                Expires = IssueAt.AddMinutes(_TokenSettings.ExpirationTime),
                SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature),
            };

            return await Task.FromResult(tokenDescriptor);
        }

        /// <summary>
        /// Generates an Refresh token base on user information and expire date.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Return <paramref name="RefreshToken"/> with the token and expire date.</returns>
        private async Task<RefreshToken> GetRefreshToken(DateTime? tokenExpireTime)
        {
            byte[] tokenKey = new byte[64];
            
            var expireTime = (tokenExpireTime is not null) ? 
                tokenExpireTime.Value.AddMinutes(_TokenSettings.RefreshTokenExpire) : 
                _DataProvider.UtcNow.AddMinutes(_TokenSettings.RefreshTokenExpire);

            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(tokenKey);
            
            return await Task.FromResult(
                new RefreshToken(
                    Convert.ToBase64String(tokenKey),
                    expireTime));
        }

    }
}
