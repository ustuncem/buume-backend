using BUUME.SharedKernel;

namespace BUUME.Domain.CampaignTypes.Events;

public record CampaignTypeUpdatedDomainEvent(Guid CampaignTypeId) : IDomainEvent;