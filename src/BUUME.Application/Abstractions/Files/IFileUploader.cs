using BUUME.SharedKernel;

namespace BUUME.Application.Abstractions.Files;

public interface IFileUploader
{
    Task<Result<FileResponse>> UploadImageFromBase64Async(string? base64Image, string mode = "business");
    Task<Result<FileResponse>> UploadPdfFromBase64Async(string? base64Pdf);
    bool DeleteFile(string filePath);
}