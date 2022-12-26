namespace LDST.Infrastructure.Models;

public record BlobStorageOptions
{
    public string ConnectionString { get; init; } = null!;
    public string ImagesContainer { get; init; } = null!;
    public string VideoContainer { get; init; } = null!;
    public string TextContainer { get; init; } = null!;
    public string UnknownContainer { get; init; } = null!;
}
