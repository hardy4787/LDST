using LDST.Domain.EFModels;

namespace LDST.Application.Features.Authentication.Shared.Models;

public record AuthenticationResult(Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Token);
