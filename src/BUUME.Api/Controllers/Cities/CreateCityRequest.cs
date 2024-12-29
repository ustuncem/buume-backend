namespace BUUME.Api.Controllers.Cities;

public record CreateCityRequest(string name, string code, Guid countryId, Guid? regionId = null);