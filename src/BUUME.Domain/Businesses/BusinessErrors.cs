using BUUME.SharedKernel;

namespace BUUME.Domain.Businesses;

public static class BusinessErrors
{
    public static Error NoLogoUploaded => Error.Failure(
        "Business.NoLogoUploaded",
        $"You need a logo to onboard.");
    
    public static Error NoTaxDocumentUploaded => Error.Failure(
        "Business.NoTaxDocumentUploaded",
        $"You need a tax document to onboard.");
    
    public static Error KvkkNotApproved => Error.Failure(
        "Business.KvkkNotApproved",
        $"You need to approve kvkk first.");
    
    public static Error NotFound => Error.NotFound(
        "Business.NotFound",
        $"İşletme bulunamadı.");
    
    public static Error ContentNotFound => Error.NotFound(
        "Business.ContentNotFound",
        $"Değiştirilmek istenen içerik bulunamadı.");
    
    public static Error ContentDeletionFailure => Error.Failure(
        "Business.ContentDeletionFailure",
        $"İçerik yükleme sırasında bir hata oluştu. Lütfen daha sonra tekrar dene.");
    
    public static Error ContentUploadFailure => Error.Failure(
        "Business.ContentDeletionFailure",
        $"İçerik yükleme sırasında bir hata oluştu. Lütfen daha sonra tekrar dene.");
}
