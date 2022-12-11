using LDST.Domain.Common.Models;

namespace LDST.Domain.Common.ValueObjects
{
    public sealed class AverageRating : ValueObject
    {
        public double Value { get; private set; }
        public int NumRatings { get; private set; }

        public AverageRating(double value, int numRatings)
        {
            Value = value;
            NumRatings = numRatings;
        }

        public static AverageRating CreateNew(double rating = 0, int numRatings = 0)
        {
            return new AverageRating(rating, numRatings);
        }

        public void AddNewRating(Rating rating) => Value = ((Value * NumRatings) + rating.Value) / ++NumRatings;

        public void RemoveRating(Rating rating) => Value = ((Value * NumRatings) - +rating.Value) / --NumRatings;

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}