using MediatR;
using Microsoft.AspNetCore.Mvc;
using PedroMoreira.API.Controllers.Common;
using PedroMoreira.Contracts.Authentication;

namespace PedroMoreira.API.Controllers
{
    [Route("Auth")]
    public class AuthenticationController : ApiController
    {
        public AuthenticationController(ILogger logger, ISender sender) : base(logger, sender) { }

        [HttpPost("signup")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            //var command = new RegisterCommand(
            //    request.FirstName,
            //    request.LastName,
            //    request.Email,
            //    request.Password);

            //var result = await _sender.Send(command);

            //return result.Match(
            //    data => Ok(data),
            //    error => Problem(error));
            return Ok();
        }

        [HttpPost("signin")]
        public async Task<IActionResult> Login(LoginRequest request)
        {

            //var command = new LoginRequest(
            //    request.Email,
            //    request.Password,
            //    request.TwoFactorCode,
            //    request.TwoFactorRecoveryCode);

            //var result = await _sender.Send(command);

            //return result.Match(
            //    data => Ok(data),
            //    error => Problem(error));
            return Ok();
        }

        [HttpPost("signout")]
        public async Task<IActionResult> Logout()
        {
            return Ok();
        }

        [HttpGet("reset-password")]
        public async Task<IActionResult> ResetPassword(string email)
        {
            return Ok();
        }

    }
}
