using ErrorOr;

namespace PedroMoreira.Domain.ErrorMessages
{
    public static partial class Errors
    {
        public static class Authentication
        {
            // General Errors
            public static Error UnauthorizedToken =>
                Error.Forbidden(
                    "Authentication.UnauthorizedToken", 
                    "Invalid Token.");

            public static Error ValueCannotBeNullOrEmpty => 
                Error.Validation(
                    "Authentication.ValueCannotBeNullOrEmpty",
                    "Value cannot be null or empty.");

            // Email Errors
            public static Error InvalidCredentials =>
                Error.Validation(
                    "Authentication.InvalidCredentials",
                    "Authentication failed. Please check your credentials and try again.");

            public static Error EmailNotConfirmed =>
                Error.Conflict(
                    "Authentication.EmailNotConfirmed",
                    "Please verify your email address to proceed.");

            public static Error InvalidEmail => 
                Error.Validation(
                    "Authentication.InvalidEmail", 
                    "This email is either invalid or already in use.");

            public static Error EmailNullOrEmpty =>
                Error.Validation(
                    "Authentication.EmailNullOrEmpty",
                    "Need to specify an email.");

            // Password Errors
            public static Error PasswordNullOrEmpty =>
                Error.Validation(
                    "Authentication.PasswordNullOrEmpty",
                    "Need to specify an password.");

            public static Error PasswordMinLenght =>
                Error.Validation(
                    "Authentication.PasswordMinLenght",
                    "Your password length must be at least 8.");

            public static Error PasswordMaxLenght =>
                Error.Validation(
                    "Authentication.PasswordMaxLenght",
                    "Your password length must be at least 16.");            
            
            public static Error PasswordUpperCase =>
                Error.Validation(
                    "Authentication.PasswordUpperCase",
                    "Your password must contain at least one uppercase letter.");

            public static Error PasswordLowerCase =>
                Error.Validation(
                    "Authentication.PasswordLowerCase",
                    "Your password must contain at least one lowercase letter.");

            public static Error PasswordWithNumber =>
                Error.Validation(
                    "Authentication.PasswordWithNumber",
                    "Your password must contain at least one number.");

            public static Error PasswordWithSpecialChar =>
                Error.Validation(
                    "Authentication.PasswordWithSpecialChar",
                    "Your password must contain at least one (!? *.).");

        }
    }
}
