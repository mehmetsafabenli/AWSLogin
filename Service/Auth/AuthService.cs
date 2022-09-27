using System.Net;
using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Extensions.CognitoAuthentication;
using AutoMapper;
using Mbis.Cognito.Abstract.Auth;
using Mbis.Cognito.Configuration.Settings;
using Mbis.Cognito.Model.Auth;
using Mbis.Cognito.Model.Token;
using Mbis.Cognito.Model.User;
using Mbis.Cognito.Result;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using IResult = Mbis.Cognito.Result.IResult;

namespace Mbis.Cognito.Service.Auth
{
    public class AuthService : IAuthService
    {
        #region Fields

        private readonly AmazonCognitoIdentityProviderClient _provider;
        private readonly CognitoUserPool _userPool;
        private readonly IMapper _mapper;
        private readonly AWS _settings;
        private readonly IConfiguration _configuration;

        #endregion

        #region Constructor

        public AuthService(IOptions<AWS> appSettings,
            IMapper mapper,
            IConfiguration configuration)
        {
            _settings = appSettings.Value;
            _provider = new AmazonCognitoIdentityProviderClient(_settings.AccessKeyId,
                _settings.AccessSecretKey, RegionEndpoint.GetBySystemName(_settings.Region));
            _mapper = mapper;
            _configuration = configuration;
            _userPool = new CognitoUserPool(_settings.UserPoolId, _settings.UserPoolClientId, _provider);
        }

        #endregion

        #region CognitoUser

        private Task<CognitoUser> GetCognitoUser(string email, string appclient, CognitoUserPool pool,
            AmazonCognitoIdentityProviderClient provider)
        {
            return Task.FromResult(new CognitoUser(email, appclient, pool, provider));
        }

        private async Task<Tuple<CognitoUser, AuthenticationResultType>> AuthenticateUserAsync(string email,
            string password)
        {
            try
            {
                var user = await GetCognitoUser(email, _settings.UserPoolClientId, _userPool, _provider);
                var authRequest = new InitiateSrpAuthRequest
                {
                    Password = password,
                };
                var authResponse = await user.StartWithSrpAuthAsync(authRequest);
                var result = authResponse.AuthenticationResult;
                return new Tuple<CognitoUser, AuthenticationResultType>(user, result);
            }
            catch (InvalidOAuthFlowException e)
            {
                throw new InvalidPasswordException(e.Message);
            }
        }

        #endregion

        #region CognitoUserPool

        public async Task<IResult> LoginUserAsync(UserLoginModel model)
        {
            try
            {
                var user = await AuthenticateUserAsync(model.Email, model.Password);

                if (user.Item1.Username == null) return Result.Result.Fail("User not found");
                var authResponseModel = new AuthResponseModel();
                authResponseModel.EmailAddress = user.Item1.UserID;
                authResponseModel.UserId = user.Item1.Username;
                authResponseModel.Tokens = new TokenModel
                {
                    IdToken = user.Item2.IdToken,
                    AccessToken = user.Item2.AccessToken,
                    ExpiresIn = user.Item2.ExpiresIn,
                    RefreshToken = user.Item2.RefreshToken
                };

                return authResponseModel.Success();
            }
            catch (Exception e)
            {
                return Result.Result.Fail("Invalid username or password");
            }
        }

        public async Task<IResult> UserLogoutAsync(UserSignOutModel model)
        {
            var request = new GlobalSignOutRequest {AccessToken = model.AccessToken};
            var response = await _provider.GlobalSignOutAsync(request);
            return response.Success("User SignOut!");
        }

        #endregion
    }
}