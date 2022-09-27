namespace Mbis.Cognito.Validations.User;

public class UserLoginValidator : UserLoginValidations
{
    public UserLoginValidator()
    {
        UserName();
        Password();
    }
}

public class UserLogoutValidator : UserLogoutValidations
{
    public UserLogoutValidator()
    {
        AccessToken();
    }
}