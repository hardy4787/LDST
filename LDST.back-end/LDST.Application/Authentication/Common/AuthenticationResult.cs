using LDST.Domain.EFModels;

namespace LDST.Application.Authentication.Common;

public record AuthenticationResult(User User, string Token);