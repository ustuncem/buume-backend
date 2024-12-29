using BUUME.Api.Requests;

namespace BUUME.Api.Controllers.Cities;

public class GetCitiesByCountryIdRequest : SearchableRequest
{
    public Guid countryId { get; set;  } = Guid.Empty;
}