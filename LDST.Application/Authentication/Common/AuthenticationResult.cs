using LDST.Domain.User;

namespace LDST.Application.Authentication.Common;

public record AuthenticationResult(User User, string Token);