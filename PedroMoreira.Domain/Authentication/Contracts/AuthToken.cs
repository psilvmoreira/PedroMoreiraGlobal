namespace PedroMoreira.Domain.Authentication.Contracts
{
    public record AuthToken(
        string TokenType,
        string Token,
        DateTime TokenExpire,
        RefreshToken RefreshToken);
}
