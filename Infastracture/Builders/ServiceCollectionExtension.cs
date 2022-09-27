using Mbis.Cognito.Abstract.Auth;
using Mbis.Cognito.Abstract.User;
using Mbis.Cognito.Service.Auth;
using Mbis.Cognito.Service.User;

namespace Mbis.Cognito.Infastracture.Builders;

public static class ServiceCollectionExtension
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
    }
}