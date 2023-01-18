namespace LDST.Application.Features.Authentication.Queries.Login;

public record AuthenticationResult(
    string Token,
    string UserName,
    bool Is2StepVerificationRequired = false,
    string? Provider = null);
