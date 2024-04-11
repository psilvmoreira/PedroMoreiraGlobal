using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PedroMoreira.API.Controllers.Common;
using PedroMoreira.Application.Common.Settings;

namespace PedroMoreira.API.Controllers
{
    [Route("User")]
    public class UserController : ApiController
    {
        private readonly ValidationSettings configuration;

        public UserController(
            ILogger<UserController> logger, 
            ISender sender,
            IOptions<ValidationSettings> configuration) 
            : base(logger, sender)
        {
            this.configuration = configuration.Value;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserInformation(string id)
        {
            return Ok();
        }

        //[Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile(string id)
        {
            return Ok(configuration.Secret);
        }

    }
}
