namespace BUUME.Api.Controllers.Districts;

public record UpdateDistrictRequest(string name, string code, Guid cityId);