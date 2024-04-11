using FluentValidation;
using PedroMoreira.Domain.ErrorMessages;

namespace PedroMoreira.Application.Common.Validator
{
    public sealed class PasswordValidator : AbstractValidator<string>
    {
        public PasswordValidator() {
            RuleFor(p => p)
                    .NotEmpty()
                        .WithErrorCode(Errors.Authentication.PasswordNullOrEmpty.Code)
                        .WithMessage(Errors.Authentication.PasswordNullOrEmpty.Description)
                    .MinimumLength(8)
                        .WithErrorCode(Errors.Authentication.PasswordMinLenght.Code)
                        .WithMessage(Errors.Authentication.PasswordMinLenght.Description)
                    .MaximumLength(16)
                        .WithErrorCode(Errors.Authentication.PasswordMaxLenght.Code)
                        .WithMessage(Errors.Authentication.PasswordMaxLenght.Description)
                    .Matches(@"[A-Z]+")
                        .WithErrorCode(Errors.Authentication.PasswordUpperCase.Code)
                        .WithMessage(Errors.Authentication.PasswordUpperCase.Description)
                    .Matches(@"[a-z]+")
                        .WithErrorCode(Errors.Authentication.PasswordLowerCase.Code)
                        .WithMessage(Errors.Authentication.PasswordLowerCase.Description)
                    .Matches(@"[0-9]+")
                        .WithErrorCode(Errors.Authentication.PasswordWithNumber.Code)
                        .WithMessage(Errors.Authentication.PasswordWithNumber.Description)
                    .Matches(@"[\!\?\*\.]+")
                        .WithErrorCode(Errors.Authentication.PasswordWithSpecialChar.Code)
                        .WithMessage(Errors.Authentication.PasswordWithSpecialChar.Description);
        }
    }
}
