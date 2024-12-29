namespace BUUME.Api.Controllers.Cities;

public record UpdateCityRequest(string name, string code, Guid countryId, Guid? regionId);