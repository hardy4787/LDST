using ErrorOr;
using LDST.Domain.Common.Models;
using LDST.Domain.Common.ValueObjects;
using LDST.Domain.Guest.ValueObjects;
using LDST.Domain.Playground.ValueObjects;

namespace LDST.Domain.Guest.Entities
{
    public sealed class GuestRating : Entity<GuestRatingId>
    {
        public PlaygroundId PlaygroundId { get; }
        public Rating Rating { get; }

        private GuestRating(PlaygroundId playgroundId, Rating rating)
            : base(GuestRatingId.CreateUnique())
        {
            PlaygroundId = playgroundId;
            Rating = rating;
        }

        public static ErrorOr<GuestRating> Create(PlaygroundId dinnerId, int rating)
        {
            var ratingValueObject = Rating.Create(rating);

            return new GuestRating(dinnerId, ratingValueObject);
        }
    }
}
