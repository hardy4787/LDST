using LDST.Domain.Bill.ValueObjects;
using LDST.Domain.Common.Models;
using LDST.Domain.GameTimeslot.ValueObjects;
using LDST.Domain.User.ValueObjects;

namespace LDST.Domain.GameReservation.ValueObjects;

public sealed class GameReservationId : ValueObject
{
    public string Value { get; }

    private GameReservationId(GameTimeslotId gameTimeslotId)
    {
        Value = $"GameReservation_{gameTimeslotId.Value}";
    }

    private GameReservationId(string value)
    {
        Value = value;
    }

    public static GameReservationId Create(GameTimeslotId gameTimeslotId)
    {
        return new GameReservationId(gameTimeslotId);
    }

    public static GameReservationId Create(string value)
    {
        return new GameReservationId(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}