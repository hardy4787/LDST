namespace LDST.Domain.EFModels;

public class HostEntity
{
    public Guid Id { get; set; }
    public string? ProfileImagePath { get; set; }

    public UserEntity User { get; set; } = null!;
    public Guid UserId { get; set; }
    public List<PlaygroundEntity> Playgrounds { get; set; } = new();
}
