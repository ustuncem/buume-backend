using BUUME.SharedKernel;

namespace BUUME.Domain.Cities;

public static class CityErrors
{
    public static Error NotFound(Guid cityId) => Error.NotFound(
        "Cities.NotFound",
        $"The city with the Id = '{cityId}' was not found");
}
