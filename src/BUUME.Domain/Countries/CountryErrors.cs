using BUUME.SharedKernel;

namespace BUUME.Domain.Countries;

public static class CountryErrors
{
    public static Error NotFound(Guid countryId) => Error.NotFound(
        "Countries.NotFound",
        $"The country with the Id = '{countryId}' was not found");
}
