using BUUME.Application.Abstractions.Messaging;

namespace BUUME.Application.CampaignTypes.CreateCampaignType;

public record CreateCampaignTypeCommand(string Name, string Code, string Description) : ICommand<Guid>;