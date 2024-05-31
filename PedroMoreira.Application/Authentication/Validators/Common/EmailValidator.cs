using FluentValidation;
using Microsoft.Extensions.Options;
using PedroMoreira.Application.Common.Settings;
using PedroMoreira.Domain.ErrorMessages;

namespace PedroMoreira.Application.Authentication.Validators.Common
{
    public sealed class EmailValidator : AbstractValidator<string>
    {

        private readonly ValidationSettings _regexConfig;

        public EmailValidator(
            IOptions<ValidationSettings> regexConfig)
        {
            _regexConfig = regexConfig.Value;

            RuleFor(x => x)
                .NotEmpty()
                    .WithErrorCode(Errors.Authentication.EmailNullOrEmpty.Code)
                    .WithMessage(Errors.Authentication.EmailNullOrEmpty.Description)
                .Matches(_regexConfig.EmailRegex)
                    .WithErrorCode(Errors.Authentication.InvalidEmail.Description)
                    .WithMessage(Errors.Authentication.InvalidEmail.Description);
        }
    }
}
