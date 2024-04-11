using ErrorOr;
using MediatR;

namespace PedroMoreira.Application.Authentication.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<RegisterResponse>>
    {
        public Task<ErrorOr<RegisterResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {


            throw new NotImplementedException();
        }
    }
}
