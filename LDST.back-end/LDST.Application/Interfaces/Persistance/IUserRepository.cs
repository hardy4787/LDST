using LDST.Domain.EFModels;

namespace LDST.Application.Interfaces.Persistance;

public interface IUserRepository
{
    UserEntity? GetUserByEmail(string email);
    Task AddAsync(UserEntity user);
}
