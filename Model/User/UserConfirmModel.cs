using Mbis.Cognito.Model.Base;

namespace Mbis.Cognito.Model.User
{
    public class UserConfirmModel : BaseRequestModel
    {
        public string UserName { get; set; }
        public string ConfirmCode { get; set; }
    }
}
