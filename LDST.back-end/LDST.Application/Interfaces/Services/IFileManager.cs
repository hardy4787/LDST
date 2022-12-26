using FileInfo = LDST.Application.Models.FileInfo;

namespace LDST.Application.Interfaces.Persistance;

public interface IFileManager
{
    Task<string> UploadFileAsync(FileInfo file, string containerPrefix);
    Task DeleteFileAsync(string fileUrl);
}
