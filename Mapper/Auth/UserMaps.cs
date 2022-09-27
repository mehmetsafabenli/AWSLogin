using Amazon.CognitoIdentityProvider.Model;
using AutoMapper;
using Mbis.Cognito.Model.User;

namespace Mbis.Cognito.Mapper.Auth
{
    public class UserMaps : Profile
    {
        public UserMaps()
        {
            CreateMap<UserSignUpModel, SignUpRequest>()
            .ForMember(dest => dest.Username, opt => { opt.MapFrom(src => src.UserName); })
            .ForMember(dest => dest.Password, opt => { opt.MapFrom(src => src.Password); })
            .ForMember(dest => dest.ClientId, opt => { opt.MapFrom(src => src.ClientId); });

            CreateMap<UserConfirmModel, ConfirmSignUpRequest>()
          .ForMember(dest => dest.Username, opt => { opt.MapFrom(src => src.UserName); })
          .ForMember(dest => dest.ConfirmationCode, opt => { opt.MapFrom(src => src.ConfirmCode); })
          .ForMember(dest => dest.ClientId, opt => { opt.MapFrom(src => src.ClientId); });
        }
    }
}

