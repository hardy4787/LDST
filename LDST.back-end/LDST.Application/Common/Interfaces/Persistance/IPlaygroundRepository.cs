using PlaygroundAggregate = LDST.Domain.EFModels.Playground;

namespace LDST.Application.Common.Interfaces.Persistance;

public interface IPlaygroundRepository
{
    Task<int> AddAsync(PlaygroundAggregate playground);
}
