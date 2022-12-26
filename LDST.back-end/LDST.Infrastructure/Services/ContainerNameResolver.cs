using LDST.Application.Interfaces.Persistance;
using LDST.Infrastructure.Models;
using Microsoft.Extensions.Options;

namespace LDST.Infrastructure.Services;
public class ContainerNameResolver : IContainerNameResolver
{
    private readonly Dictionary<string, string?> _contentTypeToContainerNameDictionary;
    private readonly BlobStorageOptions _storageOptions;

    public ContainerNameResolver(IOptions<BlobStorageOptions> storageOptions)
    {
        _storageOptions = storageOptions.Value;
        _contentTypeToContainerNameDictionary = new Dictionary<string, string?>
        {
            { KnownContentTypes.Image, _storageOptions.ImagesContainer },
            { KnownContentTypes.Text, _storageOptions.TextContainer },
            { KnownContentTypes.Video, _storageOptions.VideoContainer }
        };
    }

    public string GetContainerName(string contentType, string containerPrefix)
    {
        // Trim like "text/csv" -> "text" 
        var markerWithoutFileExtension = contentType[..contentType.IndexOf("/", StringComparison.Ordinal)];

        _contentTypeToContainerNameDictionary.TryGetValue(markerWithoutFileExtension, out var rawContainerName);

        return string.IsNullOrEmpty(rawContainerName)
            ? BuildContainerName(_storageOptions.UnknownContainer, containerPrefix)
            : BuildContainerName(rawContainerName, containerPrefix);
    }

    private static string BuildContainerName(string? containerName, string containerPrefix)
    {
        return $"{containerName}-{containerPrefix}";
    }
}
