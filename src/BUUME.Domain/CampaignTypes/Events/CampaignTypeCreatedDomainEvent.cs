using BUUME.SharedKernel;

namespace BUUME.Domain.CampaignTypes.Events;

public record CampaignTypeCreatedDomainEvent(Guid CampaignTypeId) : IDomainEvent;