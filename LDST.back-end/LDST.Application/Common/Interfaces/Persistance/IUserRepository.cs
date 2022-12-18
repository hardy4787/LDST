using LDST.Domain.EFModels;

namespace LDST.Application.Common.Interfaces.Persistance
{
    public interface IUserRepository
    {
        User? GetUserByEmail(string email);
        Task AddAsync(User user);
    }
}
