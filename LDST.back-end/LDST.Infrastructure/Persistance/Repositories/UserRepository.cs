using LDST.Application.Interfaces.Persistance;
using LDST.Domain.EFModels;
using Microsoft.EntityFrameworkCore;

namespace LDST.Infrastructure.Persistance.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context) =>
        _context = context;

    public async Task AddAsync(UserEntity user, CancellationToken cancellationToken)
    {
        _context.Users.Add(user);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<UserEntity?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _context.Users.SingleOrDefaultAsync(u => u.Email == email, cancellationToken);
    }
}
