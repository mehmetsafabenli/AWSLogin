using Mbis.Cognito.Abstract.User;
using Mbis.Cognito.Infastracture;
using Mbis.Cognito.Model.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IResult = Mbis.Cognito.Result.IResult;

namespace Mbis.Cognito.Controllers.User;

[Authorize]
[Route("api/[controller]")]
public class UserController : BaseController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost("register")]
    public async Task<IResult> SignUpAsync([FromBody] UserSignUpModel model)
    {
        var result = await _userService.CreateUserAsync(model);
        return result;
    }
}