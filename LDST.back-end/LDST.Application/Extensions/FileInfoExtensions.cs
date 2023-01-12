using Microsoft.AspNetCore.Http;
using FileInfo = LDST.Domain.FileInfo;

namespace LDST.Application.Extensions;

public static class FileInfoExtensions
{
    public static FileInfo ToFileInfo(this IFormFile file)
    {
        return new FileInfo
        {
            Content = file.OpenReadStream(),
            ContentType = file.ContentType,
            Name = file.FileName
        };
    }
}
