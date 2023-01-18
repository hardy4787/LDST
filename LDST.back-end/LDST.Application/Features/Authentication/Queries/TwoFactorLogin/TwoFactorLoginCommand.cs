using ErrorOr;
using LDST.Application.Abstractions;
using LDST.Application.Features.Authentication.Queries.Login;
using LDST.Application.Interfaces.Services;
using LDST.Application.Interfaces;
using LDST.Domain.EFModels;
using LDST.Domain.Errors;
using LDST.Domain.Mail;
using Microsoft.AspNetCore.Identity;

namespace LDST.Application.Features.Authentication.Queries.TwoFactorLogin;

public sealed class TwoFactorLoginQuery : IQuery<AuthenticationResult>
{
    public string Email { get; set; } = null!;
    public string Provider { get; set; } = null!;
    public string Token { get; set; } = null!;

    internal class Handler : IQueryHandler<TwoFactorLoginQuery, AuthenticationResult>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly UserManager<UserEntity> _userManager;

        public Handler(IJwtTokenGenerator jwtTokenGenerator, UserManager<UserEntity> userManager)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userManager = userManager;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(TwoFactorLoginQuery query, CancellationToken cancellationToken)
        {
            if (await _userManager.FindByEmailAsync(query.Email) is not UserEntity user)
            {
                return DomainErrors.Authentication.InvalidCredentials;
            }

            if (!await _userManager.VerifyTwoFactorTokenAsync(user, query.Provider, query.Token))
            {
                return DomainErrors.Authentication.InvalidCredentials;
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenerateToken(user, roles);

            return new AuthenticationResult(
                Token: token, UserName: user.UserName!);
        }
    }
}