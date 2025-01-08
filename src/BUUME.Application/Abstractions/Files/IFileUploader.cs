using BUUME.SharedKernel;

namespace BUUME.Application.Abstractions.Files;

public interface IFileUploader
{
    Task<Result<FileResponse>> UploadImageFromBase64Async(string? base64Image, string mode = "business");
}