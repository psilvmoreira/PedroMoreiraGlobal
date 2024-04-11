using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PedroMoreira.API.Controllers.Common
{
    [ApiController]
    public class ApiController(ILogger logger, ISender sender) : ControllerBase
    {
        protected readonly ILogger _logger = logger;
        protected readonly ISender _sender = sender;

        protected IActionResult Problem(List<Error> errors)
        {
            HttpContext.Items.TryAdd("Errors", errors);

            var firstError = errors.FirstOrDefault();

            var statusCode = firstError.Type switch
            {
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError,
            };

            return Problem(statusCode: statusCode, title: firstError.Description);
        }
    }
}
