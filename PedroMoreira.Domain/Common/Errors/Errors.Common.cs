using ErrorOr;

namespace PedroMoreira.Domain.ErrorMessages
{
    public static partial class Errors
    {
        public static class Common
        {
            public static Error UnknownError => 
                Error.NotFound(
                    "Common.UnknownError", 
                    "An error occurred while processing your request.");
        }
    }
}
