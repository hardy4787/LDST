using LDST.Domain.Entities;

namespace LDST.Application.Authentication.Common
{
    public record AuthenticationResult(User User, string Token);
}