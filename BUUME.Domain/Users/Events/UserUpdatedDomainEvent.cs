using BUUME.Domain.Abstractions;
using MediatR;

namespace BUUME.Domain.Users.Events;

public sealed record UserUpdatedDomainEvent(Guid UserId) : IDomainEvent;