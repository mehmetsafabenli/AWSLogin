using Mbis.Cognito.Abstract.Auth;
using Mbis.Cognito.Infastracture;
using Mbis.Cognito.Model.Auth;
using Mbis.Cognito.Model.User;
using Mbis.Cognito.Result;
using Mbis.Cognito.Validations.User;
using Microsoft.AspNetCore.Mvc;
using IResult = Mbis.Cognito.Result.IResult;

namespace Mbis.Cognito.Controllers.Identity
{
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IResult> LoginAsync([FromBody] UserLoginModel model)
        {
            var validations = new UserLoginValidator().ValidateAsync(model);
            if (!validations.Result.IsValid)
            {
                return validations.Result.Errors.Fail();
            }

            return await _authService.LoginUserAsync(model);
        }
        [HttpPost("logout")]
        public async Task<IResult> LogoutAsync([FromBody] UserSignOutModel model)
        {
            var validations = new UserLogoutValidator().ValidateAsync(model);
            if (!validations.Result.IsValid)
            {
                return validations.Result.Errors.Fail();
            }
            return await _authService.UserLogoutAsync(model);
        }
    }
}