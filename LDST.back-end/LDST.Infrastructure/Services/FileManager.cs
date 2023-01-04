using Azure.Storage.Blobs;
using Azure.Storage;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Options;
using FileInfo = LDST.Application.Models.FileInfo;
using LDST.Infrastructure.Models;
using LDST.Application.Interfaces.Persistance;
using System.ComponentModel;
using Azure.Storage.Blobs.Specialized;
using System.Reflection.Metadata;
using System.Xml.Linq;
using static System.Reflection.Metadata.BlobBuilder;

namespace LDST.Infrastructure.Services;

public class FileManager : IFileManager
{
    private readonly IContainerNameResolver _containerNameResolver;
    private readonly BlobStorageOptions _storageOptions;

    public FileManager(
        IOptions<BlobStorageOptions> storageOptions,
        IContainerNameResolver containerNameResolver)
    {
        _containerNameResolver = containerNameResolver;
        _storageOptions = storageOptions.Value;
    }

    public async Task<IEnumerable<string>> UploadFilesAsync(IEnumerable<FileInfo> files, string containerPrefix)
    {
        var options = new BlobUploadOptions
        {
            TransferOptions = new StorageTransferOptions
            {
                MaximumConcurrency = 4,
                MaximumTransferSize = 50 * 1024 * 1024 // 50mb
            }
        };

        var uploadTasks = files
            .Select(f =>
            {
                options.HttpHeaders = new BlobHttpHeaders
                {
                    ContentType = f.ContentType
                };

                return UploadFileAsync(f, options, containerPrefix);
            })
            .ToArray();

        await Task.WhenAll(uploadTasks);

        return uploadTasks.Select(t => t.Result);
    }

    public async Task DeleteFileAsync(string fileUrl)
    {
        Uri uri = new(fileUrl);
        string filePath = uri.AbsolutePath;
        string[] pathSegments = filePath.Split('/');
        string containerName = pathSegments[1];
        string fileName = pathSegments[2];

        BlobClient blob = new BlobClient(_storageOptions.ConnectionString, containerName, fileName);

        await blob.DeleteIfExistsAsync();
    }

    public async Task<string> UploadFileAsync(FileInfo file, string containerPrefix)
    {
        return await UploadFileAsync(file, new BlobUploadOptions
        {
            HttpHeaders = new BlobHttpHeaders
            {
                ContentType = file.ContentType
            }
        }, containerPrefix);
    }

    public async Task<string> UploadFileAsync(FileInfo file, BlobUploadOptions options, string containerPrefix)
    {
        try
        {
            var containerName = _containerNameResolver.GetContainerName(file.ContentType, containerPrefix);

            var blobClient = await GetBlobClient(file, containerName);

            await blobClient.UploadAsync(file.Content, options);

            return blobClient.Uri.ToString();
        }
        finally
        {
            await file.Content.DisposeAsync();
        }
    }


    private async Task<BlobClient> GetBlobClient(FileInfo file, string containerName)
    {
        var blobContainerClient = new BlobContainerClient(_storageOptions.ConnectionString, containerName);

        await blobContainerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);

        var blobClient = blobContainerClient.GetBlobClient(file.Name);

        if (await blobClient.ExistsAsync())
        {
            string newFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.Name);

            blobClient = blobContainerClient.GetBlobClient(newFileName);
        }
        return blobClient;
    }
}
