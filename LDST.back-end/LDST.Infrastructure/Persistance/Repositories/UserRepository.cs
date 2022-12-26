using LDST.Application.Interfaces.Persistance;
using LDST.Domain.EFModels;

namespace LDST.Infrastructure.Persistance.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context) =>
        _context = context;

    public async Task AddAsync(UserEntity user)
    {
        _context.Users.Add(user);

        await _context.SaveChangesAsync();
    }

    public UserEntity? GetUserByEmail(string email)
    {
        return _context.Users.SingleOrDefault(u => u.Email == email);
    }
}
