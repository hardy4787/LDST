using LDST.Application.Common.Interfaces.Persistance;
using LDST.Domain.EFModels;

namespace LDST.Infrastructure.Persistance;

public class PlaygroundRepository : IPlaygroundRepository
{
    private readonly AppDbContext _context;

    public PlaygroundRepository(AppDbContext context) =>
        _context = context;

    public async Task<int> AddAsync(Playground playground)
    {
        _context.Playgrounds.Add(playground);

        await _context.SaveChangesAsync();

        return playground.Id;
    }
}
