namespace BUUME.Api.Controllers.Districts;

public record CreateDistrictRequest(string name, string code, Guid cityId);