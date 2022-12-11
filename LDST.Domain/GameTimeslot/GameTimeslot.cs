using LDST.Domain.Common.Models;
using LDST.Domain.GameReservation.ValueObjects;
using LDST.Domain.GameTimeslot.Enums;
using LDST.Domain.GameTimeslot.ValueObjects;
using LDST.Domain.Playground.ValueObjects;

namespace LDST.Domain.GameTimeslot;

public sealed class GameTimeslot : AggregateRoot<GameTimeslotId>
{
    public PlaygroundId PlaygroundId { get; }
    public GameTimeslotStatus GameTimeslotStatus { get; }
    public GameReservationId? GameReservationId { get; } // TODO: How to use it if it is immutable

    private GameTimeslot(PlaygroundId playgroundId, GameTimeslotStatus gameTimeslotStatus) : base(GameTimeslotId.CreateUnique())
    {
        PlaygroundId = playgroundId;
        GameTimeslotStatus = gameTimeslotStatus;
    }

    public static GameTimeslot Create(PlaygroundId playgroundId, GameTimeslotStatus? gameTimeslotStatus = null)
    {
        return new GameTimeslot(playgroundId, gameTimeslotStatus ?? GameTimeslotStatus.Free);
    }
}
