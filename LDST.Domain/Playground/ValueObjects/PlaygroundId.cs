using LDST.Domain.Common.Models;

namespace LDST.Domain.Playground.ValueObjects;

public sealed class PlaygroundId : ValueObject
{
    public Guid Value { get; }

    private PlaygroundId(Guid value)
    {
        Value = value;
    }

    public static PlaygroundId CreateUnique()
    {
        return new PlaygroundId(new Guid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}