using BUUME.SharedKernel;

namespace BUUME.Domain.TaxOffices;

public static class TaxOfficeErrors
{
    public static Error NotFound(Guid taxOfficeId) => Error.NotFound(
        "TaxOffices.NotFound",
        $"The tax office with the Id = '{taxOfficeId}' was not found");
}
