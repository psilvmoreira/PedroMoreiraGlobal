namespace PedroMoreira.Application.Authentication.Commands.Login
{
    public class AuthenticationResponse
    {
        public string Token { get; set; }
        public DateTime Expire { get; set; }
        public string Refresh { get; set; }
        public DateTime RefreshExpire { get; set; }
    }
}
