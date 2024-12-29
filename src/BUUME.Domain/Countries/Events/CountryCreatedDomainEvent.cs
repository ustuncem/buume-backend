using BUUME.SharedKernel;

namespace BUUME.Domain.Countries.Events;

public sealed record CountryCreatedDomainEvent(Guid CountryId) : IDomainEvent;
