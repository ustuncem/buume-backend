using BUUME.Application.Abstractions.Files;
using BUUME.SharedKernel;
using iText.Kernel.Pdf;
using Microsoft.Extensions.Options;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;

namespace BUUME.Infrastructure.Files;

internal sealed class FileUploader(IOptions<FileOptions> fileOptions) : IFileUploader
{
    private static readonly int MaxImageWidth = 500;
    private static readonly int MaxFileSize = 10 * 1024 * 1024;
    
    public async Task<Result<FileResponse>> UploadImageFromBase64Async(string? base64Image, string mode = "business")
    {
        if (string.IsNullOrWhiteSpace(base64Image)) return Result.Failure<FileResponse>(FileErrors.Base64StringEmpty);
        var uploadPath = mode == "business" ? fileOptions.Value.BusinessPath : fileOptions.Value.ProfilePath;
        
        try
        {
            var imageBytes = Convert.FromBase64String(base64Image);
            var isImageValid = imageBytes.Length > 0 && IsValidImage(imageBytes);
            
            if(!isImageValid) return Result.Failure<FileResponse>(FileErrors.NotValid);
            
            var image = Image.DetectFormat(imageBytes);

            var extension = image.Name.ToLower();
            var mimeType = image.DefaultMimeType;
            var imageName = $"{Guid.NewGuid()}.{extension}";
            double imageSizeInMb = imageBytes.Length / (1024.0 * 1024.0);
            
            var path = HandleDirectoryAndPathCreation(uploadPath, imageName);
            
            var hasUploadedSuccessfully = await UploadAndOptimizeImage(imageBytes, path);
            
            if(!hasUploadedSuccessfully) return Result.Failure<FileResponse>(FileErrors.NotValid);
            
            return new FileResponse(imageName, imageSizeInMb, path, mimeType);
        }
        catch (Exception)
        {
            return Result.Failure<FileResponse>(FileErrors.UploadError);
        }
    }

    public async Task<Result<FileResponse>> UploadPdfFromBase64Async(string? base64Pdf)
    {
        if (string.IsNullOrWhiteSpace(base64Pdf)) return Result.Failure<FileResponse>(FileErrors.Base64StringEmpty);
        try
        {
            var pdfBytes = Convert.FromBase64String(base64Pdf);
            var isPdfValid = pdfBytes.Length > 0 && pdfBytes.Length <= MaxFileSize;
            if(!isPdfValid) return Result.Failure<FileResponse>(FileErrors.TooLarge);
            
            using var memoryStream = new MemoryStream(pdfBytes);
            using var pdfReader = new PdfReader(memoryStream);
            using var pdfDoc = new PdfDocument(pdfReader);
            var pdfName = $"{Guid.NewGuid()}.pdf";
            var pdfSizeInMb = pdfBytes.Length / (1024.0 * 1024.0);
            var path = HandleDirectoryAndPathCreation(fileOptions.Value.DocumentsPath, pdfName);
            await File.WriteAllBytesAsync(path, pdfBytes);
            
            return new FileResponse(pdfName, pdfSizeInMb, path, "application/pdf");
        }
        catch (Exception)
        {
            return Result.Failure<FileResponse>(FileErrors.UploadError);
        }
    }

    public bool DeleteFile(string filePath)
    {
        try
        {
            if(File.Exists(filePath)) File.Delete(filePath);
            return true;

        }
        catch (Exception e)
        {
            return false;
        }
    }

    private static string HandleDirectoryAndPathCreation(string uploadPath, string fileName)
    {
        var folderPath = Path.Combine(Directory.GetCurrentDirectory(), uploadPath);
        if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
        
        var path = Path.Combine(folderPath, fileName);
        return path;
    }
    
    private static bool IsValidImage(byte[] imageBytes)
    {
        try
        {
            if(imageBytes.Length > MaxFileSize) return false;
            
            using var image = Image.Load(imageBytes);
            var format = image.Metadata.DecodedImageFormat?.Name;
            return format is "JPEG" or "PNG" or "WEBP";
        }
        catch (Exception)
        {
            return false;
        }
    }

    private static async Task<bool> UploadAndOptimizeImage(byte[] imageBytes, string filePath)
    {
        using var stream = new MemoryStream(imageBytes);
        using var image = await Image.LoadAsync(stream);
        
        var maxImageWidth = image.Width < MaxImageWidth ? image.Width : MaxImageWidth;
        var aspectRatio = (double)image.Width / image.Height;

        var resizedHeight = (int)(maxImageWidth / aspectRatio);
        var formatName = image.Metadata.DecodedImageFormat?.Name ?? "";
        
        image.Mutate(x => x.Resize(new ResizeOptions
        {
            Mode = ResizeMode.Max,
            Size = new Size(maxImageWidth, resizedHeight)
        }));

        switch (formatName)
        {
            case "JPEG":
                await image.SaveAsync(filePath, new SixLabors.ImageSharp.Formats.Jpeg.JpegEncoder { Quality = 75 });
                break;
            case "PNG":
                await image.SaveAsync(filePath,
                    new PngEncoder { CompressionLevel = PngCompressionLevel.DefaultCompression });
                break;
            case "WEBP":
                await image.SaveAsync(filePath, new WebpEncoder { Quality = 75 });
                break;
            default:
                throw new ApplicationException($"Unsupported image format: {formatName}");
        }

        return true;
    }
}