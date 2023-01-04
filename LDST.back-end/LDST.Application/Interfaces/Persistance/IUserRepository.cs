using LDST.Domain.EFModels;

namespace LDST.Application.Interfaces.Persistance;

public interface IUserRepository
{
    Task<UserEntity?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
    Task AddAsync(UserEntity user, CancellationToken cancellationToken);
}
