
using ErrorOr;

namespace PedroMoreira.Domain.ErrorMessages
{
    public static partial class Errors
    {
        public static class Member
        {
            // First Name
            public static Error FirstNameCannotBeNullOrEmpty =>
                Error.Validation(
                    "Authentication.FirstNameCannotBeNullOrEmpty",
                    "First Name cannot be empty.");

            // Last Name
            public static Error LastNameCannotBeNullOrEmpty =>
                Error.Validation(
                    "Authentication.LastNameCannotBeNullOrEmpty",
                    "Last Name cannot be empty.");
        }
    }
}
