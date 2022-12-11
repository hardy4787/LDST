using LDST.Domain.Common.Models;
using LDST.Domain.User.ValueObjects;

namespace LDST.Domain.User;

public sealed class User : AggregateRoot<UserId>
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; } // TODO: Hash this

    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    private User(string firstName, string lastName, string email, string password, UserId? userId = null)
        : base(userId ?? UserId.CreateUnique())
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
    }

    public static User Create(string firstName, string lastName, string email, string password)
    {
        // TODO: enforce invariants
        return new User(
            firstName,
            lastName,
            email,
            password);
    }
}