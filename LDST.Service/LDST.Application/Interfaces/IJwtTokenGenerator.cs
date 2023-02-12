using LDST.Domain.EFModels;

namespace LDST.Application.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(UserEntity user, IList<string> roles);
}
