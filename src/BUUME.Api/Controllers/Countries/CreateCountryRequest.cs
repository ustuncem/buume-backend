namespace BUUME.Api.Controllers.Countries;

public record CreateCountryRequest(string name, string code, bool hasRegion);