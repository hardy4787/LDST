using LDST.Domain.EFModels;

namespace LDST.Application.Common.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
