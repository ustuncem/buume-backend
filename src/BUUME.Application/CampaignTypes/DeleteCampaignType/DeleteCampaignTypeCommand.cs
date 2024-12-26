using BUUME.Application.Abstractions.Messaging;

namespace BUUME.Application.CampaignTypes.DeleteCampaignType;

public record DeleteCampaignTypeCommand(Guid Id) : ICommand<bool>;