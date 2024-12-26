namespace BUUME.Api.Controllers.CampaignTypes;

public record CreateCampaignTypeRequest(string name, string code, string? description);