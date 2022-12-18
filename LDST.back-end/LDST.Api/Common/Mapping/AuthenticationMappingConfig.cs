using LDST.Application.Authentication.Commands.Register;
using LDST.Application.Authentication.Common;
using LDST.Application.Authentication.Queries.Login;
using LDST.Contracts.Authentication;
using Mapster;

namespace LDST.Api.Common.Mapping
{
    public class AuthenticationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterRequest, RegisterCommand>();
            config.NewConfig<LoginRequest, LoginQuery>();
            config.NewConfig<AuthenticationResult, AuthenticationResponse>()
                .Map(dest => dest, src => src.User);
        }
    }
}