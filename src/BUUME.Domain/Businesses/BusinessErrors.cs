using BUUME.SharedKernel;

namespace BUUME.Domain.Businesses;

public static class BusinessErrors
{
    public static Error NoLogoUploaded => Error.Failure(
        "Business.NoLogoUploaded",
        $"You need a logo to onboard.");
    
    public static Error KvkkNotApproved => Error.Failure(
        "Business.KvkkNotApproved",
        $"You need to approve kvkk first.");
    
    public static Error NotFound => Error.NotFound(
        "Business.NotFound",
        $"Business not found.");
}
