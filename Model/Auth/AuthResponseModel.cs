using Mbis.Cognito.Model.Token;

namespace Mbis.Cognito.Model.Auth
{
    public class AuthResponseModel 
    {
        public string EmailAddress { get; set; }
        public string UserId { get; set; }
        public TokenModel Tokens { get; set; }
    }
}