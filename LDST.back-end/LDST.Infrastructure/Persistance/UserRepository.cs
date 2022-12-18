using LDST.Application.Common.Interfaces.Persistance;
using LDST.Domain.EFModels;

namespace LDST.Infrastructure.Persistance;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context) =>
    _context = context;

    public async Task AddAsync(User user)
    {
        _context.Users.Add(user);

        await _context.SaveChangesAsync();
    }

    public User? GetUserByEmail(string email)
    {
        return _context.Users.SingleOrDefault(u => u.Email == email);
    }
}
