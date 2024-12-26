using BUUME.Application.Abstractions.Messaging;

namespace BUUME.Application.CampaignTypes.UpdateCampaignType;

public record UpdateCampaignTypeCommand(Guid Id, string Name, string Code, string Description) : ICommand<Guid>;