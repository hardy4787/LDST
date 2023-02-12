using ErrorOr;
using LDST.Domain.EFModels;
using LDST.Application.Interfaces.Persistance;
using LDST.Application.Interfaces;
using LDST.Domain.Errors;
using LDST.Application.Abstractions;
using Microsoft.AspNetCore.Identity;
using LDST.Application.Interfaces.Services;
using LDST.Domain.Mail;
using Microsoft.AspNetCore.Mvc;
using static LDST.Domain.Errors.DomainErrors;

namespace LDST.Application.Features.Authentication.Queries.Login;

public class LoginQuery : IQuery<AuthenticationResult>
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;

    public sealed class Handler : IQueryHandler<LoginQuery, AuthenticationResult>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IEmailSender _emailSender;

        public Handler(IJwtTokenGenerator jwtTokenGenerator, UserManager<UserEntity> userManager, IEmailSender emailSender)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            if ((await _userManager.FindByEmailAsync(query.Email)) is not UserEntity user)
            {
                return DomainErrors.Authentication.InvalidCredentials;
            }
            
            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                return DomainErrors.Authentication.EmailIsNotConfirmed;
            }

            if (!await _userManager.CheckPasswordAsync(user, query.Password))
            {
                return DomainErrors.Authentication.InvalidCredentials;
            }

            if (await _userManager.GetTwoFactorEnabledAsync(user))
            {
                return await GenerateOTPFor2StepVerification(user);
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenerateToken(user, roles);

            return new AuthenticationResult(
                Token: token, UserName: user.UserName!, UserId: user.Id);
        }

        private async Task<ErrorOr<AuthenticationResult>> GenerateOTPFor2StepVerification(UserEntity user)
        {
            var providers = await _userManager.GetValidTwoFactorProvidersAsync(user);

            if (!providers.Contains("Email"))
            {
                return DomainErrors.Authentication.InvalidCredentials;
            }
            var token = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");
            var message = new Message
            {
                To = new string[] { user.Email! },
                Subject = "Authentication token",
                Content = token
            };

            await _emailSender.SendEmailAsync(message);

            return new AuthenticationResult(
                Token: token, 
                UserName: user.UserName!,
                UserId: user.Id, 
                Is2StepVerificationRequired: true, 
                Provider: "Email");
        }
    }
}