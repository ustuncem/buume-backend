using BUUME.SharedKernel;

namespace BUUME.Infrastructure.Files;

internal static class FileErrors
{
    public static readonly Error NotValid = new(
        "File.NotValid",
        "The file isn't valid", ErrorType.Failure);
    
    public static readonly Error UploadError = new(
        "File.UploadError",
        "The file cant be uploaded", ErrorType.Failure);
    
    public static readonly Error TooLarge = new(
        "File.TooLarge",
        "The file size is too large", ErrorType.Failure);
    
    public static readonly Error Base64StringEmpty = new(
        "File.Base64StringEmpty",
        "The base64 string cant be empty", ErrorType.Failure);
    
    public static readonly Error UnknowError = new(
        "File.UnknowError",
        "Beklenmeyen bir hata oluştu.", ErrorType.Failure);
}