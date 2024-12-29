using BUUME.Api.Requests;

namespace BUUME.Api.Controllers.Districts;

public class GetDistrictsByCityIdRequest : SearchableRequest
{
    public Guid cityId { get; set;  } = Guid.Empty;
}