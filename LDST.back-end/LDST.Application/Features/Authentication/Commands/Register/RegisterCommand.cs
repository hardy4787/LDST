using LDST.Domain.EFModels;
using LDST.Application.Interfaces.Persistance;
using LDST.Application.Interfaces;
using LDST.Application.Features.Authentication.Shared.Models;
using LDST.Application.Abstractions;
using ErrorOr;
using LDST.Domain.Errors;

namespace LDST.Application.Features.Authentication.Commands.Register;

public class RegisterCommand : ICommand<AuthenticationResult>
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;

    public sealed class Handler : ICommandHandler<RegisterCommand, AuthenticationResult>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;
        public Handler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            if ((await _userRepository.GetUserByEmailAsync(command.Email, cancellationToken)) is not null)
            {
                return DomainErrors.User.DuplicateEmail;
            }

            var user = new UserEntity { FirstName = command.FirstName, LastName = command.LastName, Email = command.Email, Password = command.Password };

            await _userRepository.AddAsync(user, cancellationToken);

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