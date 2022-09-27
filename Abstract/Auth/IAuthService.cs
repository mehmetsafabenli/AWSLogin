using Mbis.Cognito.Model.Auth;
using Mbis.Cognito.Model.User;
using IResult = Mbis.Cognito.Result.IResult;

namespace Mbis.Cognito.Abstract.Auth
{
    public interface IAuthService
    {
        #region Login

        Task<IResult> LoginUserAsync(UserLoginModel model);
        Task<IResult> UserLogoutAsync(UserSignOutModel model);

        #endregion
    }
}