using LDST.Domain.Common.Models;
using LDST.Domain.Common.ValueObjects;
using LDST.Domain.GameTimeslot.ValueObjects;
using LDST.Domain.Playground.ValueObjects;
using LDST.Domain.User.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LDST.Domain.Playground;

public sealed class Playground : AggregateRoot<PlaygroundId>
{
    private readonly List<GameTimeslotId> _gameTimeslotIds = new();

    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Sport { get; private set; }
    public Location Location { get; private set; }
    public AverageRating AverageRating { get; private set; }
    public Uri TitlePhoto { get; private set; }
    public List<Uri> Photos { get; private set; }

    public IReadOnlyList<GameTimeslotId> GameTimeslotIds => _gameTimeslotIds.AsReadOnly();

    private Playground(
        PlaygroundId playgroundId,
        string name, string description, string sport, Location location, AverageRating averageRating, Uri titlePhoto, List<Uri> photos) : base(playgroundId)
    {
        Name = name;
        Description = description;
        Sport = sport;
        Location = location;
        AverageRating = averageRating;
        TitlePhoto = titlePhoto;
        Photos = photos;
    }

    public static Playground Create(string name, string description, string sport, Location location, Uri titlePhoto, List<Uri>? photos = null)
    {
        return new Playground(
            PlaygroundId.CreateUnique(), name, description, sport, location,
            AverageRating.CreateNew(), titlePhoto, photos ?? new());
    }

}