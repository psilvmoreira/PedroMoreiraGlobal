namespace PedroMoreira.Domain.Authentication.Contracts
{
    public record RefreshToken
    (
        string Token,
        DateTime Expire
    );
}
