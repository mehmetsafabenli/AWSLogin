using Mbis.Cognito.Model.User;

namespace Mbis.Cognito.Abstract.User;

public interface IUserService
{
    #region Create

    Task<Result.IResult> CreateUserAsync(UserSignUpModel model);

    #endregion

    #region Confirm

    Task<Result.IResult> ConfirmUserAsync(UserConfirmModel model);

    #endregion
}