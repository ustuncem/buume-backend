using BUUME.SharedKernel;

namespace BUUME.Domain.CampaignTypes;

public static class CampaignTypeErrors
{
    public static Error NotFound(Guid campaignTypeId) => Error.NotFound(
        "CampaignTypes.NotFound",
        $"The campaign type with the Id = '{campaignTypeId}' was not found");
}
