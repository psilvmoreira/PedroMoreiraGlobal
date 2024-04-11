
namespace PedroMoreira.Application.Common.Settings
{
    public class ValidationSettings
    {
        public const string Name = nameof(ValidationSettings);

        public string EmailRegex { get; set; }
        public string Secret { get; set; }
    }
}
