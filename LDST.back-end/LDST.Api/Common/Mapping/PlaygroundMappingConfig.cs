using LDST.Application.Authentication.Queries.Login;
using LDST.Application.Playground.Commands.CreateCommand;
using LDST.Contracts;
using Mapster;

namespace LDST.Api.Common.Mapping
{
    public class PlaygroundMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<(CreatePlaygroundRequest Request, string HostId), CreatePlaygroundCommand>()
                .Map(dest => dest.HostId, src => src.HostId)
                .Map(dest => dest, src => src.Request);

        }
    }
}
