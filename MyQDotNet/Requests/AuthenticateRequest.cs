namespace MyQDotNet.Requests
{
    public class AuthenticateRequest
    {
        public AuthenticateRequest(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; }
        public string Password { get; }
        
        
    }
}