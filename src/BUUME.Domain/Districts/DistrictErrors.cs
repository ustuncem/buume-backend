using BUUME.SharedKernel;

namespace BUUME.Domain.Districts;

public static class DistrictErrors
{
    public static Error NotFound(Guid districtId) => Error.NotFound(
        "Districts.NotFound",
        $"The district with the Id = '{districtId}' was not found");
}
