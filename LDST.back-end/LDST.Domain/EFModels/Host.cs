namespace LDST.Domain.EFModels;

public class Host
{
    public Guid Id { get; set; }
    public string? ProfileImagePath { get; set; } = null!;

    public User User { get; set; } = null!;
    public Guid UserId { get; set; }
    public List<Playground> Playgrounds { get; set; } = new();
}
