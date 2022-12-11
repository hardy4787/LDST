using LDST.Domain.Common.Models;
using LDST.Domain.Playground.ValueObjects;

namespace LDST.Domain.GameTimeslot.ValueObjects;

public sealed class GameTimeslotId : ValueObject
{
    public Guid Value { get; }

    private GameTimeslotId(Guid value)
    {
        Value = value;
    }

    public static GameTimeslotId CreateUnique()
    {
        return new GameTimeslotId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}