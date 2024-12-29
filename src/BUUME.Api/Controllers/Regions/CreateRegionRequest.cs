namespace BUUME.Api.Controllers.Countries;

public record CreateRegionRequest(string name, Guid countryId);