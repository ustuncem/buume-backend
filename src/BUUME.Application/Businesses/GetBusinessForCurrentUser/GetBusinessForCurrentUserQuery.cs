using BUUME.Application.Abstractions.Messaging;

namespace BUUME.Application.Businesses.GetBusinessForCurrentUser;

public sealed record GetBusinessForCurrentUserQuery() : IQuery<BusinessResponse>;