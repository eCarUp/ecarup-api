namespace ecarupGrpcWebExample.Server
{
    public class AuthConstants
    {
        private const string serverAddress = "https://api.smart-me.com";

        public const string AuthenticationTheme = "myAuthTheme";
        public static readonly string AuthorizationEndpoint = serverAddress + "/oauth/authorize";
        public static readonly string TokenEndpoint = serverAddress + "/oauth/token";
        public static readonly string UserInformationEndpoint = serverAddress + "/user";        
    }
}
