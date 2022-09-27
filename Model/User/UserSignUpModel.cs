using Mbis.Cognito.Model.Base;

namespace Mbis.Cognito.Model.User
{
    public class UserSignUpModel : BaseRequestModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string ClientName { get; set; }
        public Dictionary<string, string> Attributes { get; set; }

    }
}
