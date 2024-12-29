using BUUME.Api.Requests;

namespace BUUME.Api.Controllers.Regions;

public class GetRegionsByCountryIdRequest : SearchableRequest
{
    public Guid countryId { get; set;  } = Guid.Empty;
}