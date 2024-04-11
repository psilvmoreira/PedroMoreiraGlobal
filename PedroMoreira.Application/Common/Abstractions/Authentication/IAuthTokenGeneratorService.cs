using Microsoft.IdentityModel.Tokens;
using PedroMoreira.Domain.Authentication.Contracts;
using PedroMoreira.Domain.Members.Entities;

namespace PedroMoreira.Application.Common.Abstractions.Authentication
{
    public interface IAuthTokenGeneratorService
    {

        Task<AuthToken> GetAuthToken(Member user, List<string> roles);

        Task<bool> ValidateToken(Member? user, AuthToken? token);

        Task<TokenValidationResult?> GetClaimsPrincipal(string token);
    }
}
