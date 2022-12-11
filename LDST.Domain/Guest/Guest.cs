using LDST.Domain.Bill.ValueObjects;
using LDST.Domain.Common.Models;
using LDST.Domain.Guest.Entities;
using LDST.Domain.User.ValueObjects;

namespace LDST.Domain.User
{
    public sealed class Guest : AggregateRoot<GuestId>
    {
        private readonly List<BillId> _billIds = new();
        private readonly List<GuestRating> _ratings = new();

        public string FirstName { get; }
        public string LastName { get; }
        public Uri ProfileImage { get; }
        public UserId UserId { get; }

        public IReadOnlyList<BillId> BillIds => _billIds.AsReadOnly(); // TODO: remove or implement
        public IReadOnlyList<GuestRating> Ratings => _ratings.AsReadOnly(); // TODO: remove or implement

        private Guest(string firstName, string lastName, Uri profileImage, UserId userId) : base(GuestId.Create(userId))
        {
            FirstName = firstName;
            LastName = lastName;
            ProfileImage = profileImage;
            UserId = userId;
        }

        public static Guest Create(string firstName, string lastName, Uri profileImage, UserId userId)
        {
            return new Guest(
                firstName, lastName, profileImage, userId);
        }
    }
}