using LDST.Domain.Common.Models;

namespace LDST.Domain.User.ValueObjects
{
    public sealed class GuestId : ValueObject
    {
        public string Value { get; }

        private GuestId(UserId userId)
        {
            Value = $"Guest_{userId.Value}";
        }

        public static GuestId Create(UserId userId)
        {
            return new GuestId(userId);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}