namespace LDST.Domain.EFModels;

public class UserEntity
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!; // TODO: Hash this

    public GuestEntity? Guest { get; set; }
    public HostEntity? Host { get; set; }
}