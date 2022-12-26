namespace LDST.Application.Models;

public record FileInfo
{
    public string Name { get; init; } = null!;
    public string ContentType { get; init; } = null!;
    public Stream Content { get; init; } = null!;
}