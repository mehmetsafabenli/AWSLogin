using FluentValidation;
using Mbis.Cognito.Errors;
using Mbis.Cognito.Model.User;

namespace Mbis.Cognito.Validations.User
{
    public class UserLoginValidations : AbstractValidator<UserLoginModel>
    {
        protected void UserName() => RuleFor(x => x.Email).NotEmpty().WithMessage(DefaultErrorCodes.EntityEmpty);
        protected void Password() => RuleFor(x => x.Password).NotEmpty().WithMessage(DefaultErrorCodes.EntityEmpty);
    }
    public class UserLogoutValidations : AbstractValidator<UserSignOutModel>
    {
       protected void AccessToken() => RuleFor(x => x.AccessToken).NotEmpty().WithMessage(DefaultErrorCodes.EntityEmpty);
    }
}