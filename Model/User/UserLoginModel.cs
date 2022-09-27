using Mbis.Cognito.Model.Base;

namespace Mbis.Cognito.Model.User
{
    public class UserLoginModel : BaseRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public int TokenLifeTime { get; set; }
    }
}
