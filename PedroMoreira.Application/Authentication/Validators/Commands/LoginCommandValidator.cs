using FluentValidation;
using Microsoft.Extensions.Options;
using PedroMoreira.Application.Authentication.Commands.Login;
using PedroMoreira.Application.Authentication.Validators.Common;
using PedroMoreira.Application.Common.Settings;
using PedroMoreira.Domain.ErrorMessages;


namespace PedroMoreira.Application.Authentication.Validators.Commands
{
    public sealed class LoginCommandValidator :
        AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator(IOptions<ValidationSettings> regexConfig)
        {
            RuleFor(a => a.Username)
                .NotEmpty()
                    .WithErrorCode(Errors.Authentication.EmailNullOrEmpty.Code)
                    .WithMessage(Errors.Authentication.EmailNullOrEmpty.Description)
                .NotNull()
                    .WithErrorCode(Errors.Authentication.EmailNullOrEmpty.Code)
                    .WithMessage(Errors.Authentication.EmailNullOrEmpty.Description)
                .SetValidator(a => new EmailValidator(regexConfig));

            RuleFor(a => a.Password)
                .NotEmpty()
                    .WithErrorCode(Errors.Authentication.PasswordNullOrEmpty.Code)
                    .WithMessage(Errors.Authentication.PasswordNullOrEmpty.Description)
                .NotNull()
                    .WithErrorCode(Errors.Authentication.PasswordNullOrEmpty.Code)
                    .WithMessage(Errors.Authentication.PasswordNullOrEmpty.Description);
        }
    }
}
