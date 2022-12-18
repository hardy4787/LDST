namespace LDST.Domain.EFModels;

public class User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!; // TODO: Hash this

    public Guest? Guest { get; set; }
    public Host? Host { get; set; }
}