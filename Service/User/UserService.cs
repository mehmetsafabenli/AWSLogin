using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Extensions.CognitoAuthentication;
using AutoMapper;
using Mbis.Cognito.Abstract.User;
using Mbis.Cognito.Configuration.Settings;
using Mbis.Cognito.Extensions;
using Mbis.Cognito.Model.User;
using Mbis.Cognito.Result;
using Mbis.Cognito.Validations.Auth;
using Mbis.Cognito.Validations.User;
using Microsoft.Extensions.Options;

namespace Mbis.Cognito.Service.User;

public class UserService : IUserService
{
    private readonly AmazonCognitoIdentityProviderClient _provider;
    private readonly CognitoUserPool _userPool;
    private readonly IMapper _mapper;
    private readonly AWS _settings;

    public UserService(IOptions<AWS> appSettings, IMapper mapper)
    {
        _settings = appSettings.Value;
        _provider = new AmazonCognitoIdentityProviderClient(_settings.AccessKeyId,
            _settings.AccessSecretKey, RegionEndpoint.GetBySystemName(_settings.Region));
        _mapper = mapper;
        _userPool = new CognitoUserPool(_settings.UserPoolId, _settings.UserPoolClientId, _provider);
    }

    public async Task<Result.IResult> ConfirmUserAsync(UserConfirmModel model)
    {
        try
        {
            var request = _mapper.Map<ConfirmSignUpRequest>(model);
            var response = await _provider.ConfirmSignUpAsync(request);
            return response.Success(response.HttpStatusCode);
        }
        catch (Exception e)
        {
            return Result.Result.Fail(e.Message);
        }
    }

    public async Task<Result.IResult> CreateUserAsync(UserSignUpModel model)
    {
        try
        {
            var request = _mapper.Map<SignUpRequest>(model);
            request.AddAttribute(model.Attributes);
            var response = await _provider.SignUpAsync(request);
            return response.Success(response.HttpStatusCode);
        }
        catch (Exception e)
        {
            return Result.Result.Fail(e.Message);
        }
    }
}