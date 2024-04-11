namespace PedroMoreira.Application.Authentication.Commands.Register
{
    public record RegisterResponse(
        string Email,
        bool IsSucceed
    );
}
