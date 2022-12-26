using ErrorOr;
using LDST.Domain.EFModels;
using LDST.Application.Interfaces.Persistance;
using LDST.Application.Interfaces;
using LDST.Application.Features.Authentication.Shared.Models;
using LDST.Domain.Errors;
using LDST.Application.Abstractions;

namespace LDST.Application.Features.Authentication.Queries.Login;

public class LoginQuery : IQuery<AuthenticationResult>
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;

    public sealed class Handler : IQueryHandler<LoginQuery, AuthenticationResult>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;
        public Handler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            if (_userRepository.GetUserByEmail(query.Email) is not UserEntity user)
            {
                return DomainErrors.Authentication.InvalidCredentials;
            }

            if (user.Password != query.Password)
            {
                return new[] { DomainErrors.Authentication.InvalidCredentials };
            }

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user.Id,
                user.FirstName,
                user.LastName,
                user.Email,
                token);
        }
    }
}