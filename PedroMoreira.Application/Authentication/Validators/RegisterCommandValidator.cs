using FluentValidation;
using Microsoft.Extensions.Options;
using PedroMoreira.Application.Authentication.Commands.Register;
using PedroMoreira.Application.Common.Settings;
using PedroMoreira.Application.Common.Validator;
using PedroMoreira.Domain.ErrorMessages;

namespace PedroMoreira.Application.Authentication.Validators
{
    public sealed class RegisterCommandValidator 
        : AbstractValidator<RegisterCommand>
    {

        public RegisterCommandValidator(IOptions<ValidationSettings> regexConfig)
        {

            RuleFor(x => x.Email)
                .SetValidator(new EmailValidator(regexConfig));

            RuleFor(x => x.Password)
                .SetValidator(new PasswordValidator());

            RuleFor(x => x.LastName)
                .NotNull()
                    .WithErrorCode(Errors.Member.FirstNameCannotBeNullOrEmpty.Code)
                    .WithMessage(Errors.Member.FirstNameCannotBeNullOrEmpty.Description)
                .NotEmpty()
                    .WithErrorCode(Errors.Member.FirstNameCannotBeNullOrEmpty.Code)
                    .WithMessage(Errors.Member.FirstNameCannotBeNullOrEmpty.Description);

            RuleFor(x => x.LastName)
                .NotNull()
                    .WithErrorCode(Errors.Member.LastNameCannotBeNullOrEmpty.Code)
                    .WithMessage(Errors.Member.LastNameCannotBeNullOrEmpty.Description)
                .NotEmpty()
                    .WithErrorCode(Errors.Member.LastNameCannotBeNullOrEmpty.Code)
                    .WithMessage(Errors.Member.LastNameCannotBeNullOrEmpty.Description);
        }
    }
}
