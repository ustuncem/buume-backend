using BUUME.SharedKernel;

namespace BUUME.Domain.Regions;

public static class RegionErrors
{
    public static Error NotFound(Guid regionId) => Error.NotFound(
        "Regions.NotFound",
        $"The region with the Id = '{regionId}' was not found");
}
