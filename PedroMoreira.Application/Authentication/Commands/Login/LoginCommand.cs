using ErrorOr;
using MediatR;

namespace PedroMoreira.Application.Authentication.Commands.Login
{
    public record LoginCommand(
        string Username,
        string Password
    ): IRequest<ErrorOr<AuthenticationResponse>>;
}
