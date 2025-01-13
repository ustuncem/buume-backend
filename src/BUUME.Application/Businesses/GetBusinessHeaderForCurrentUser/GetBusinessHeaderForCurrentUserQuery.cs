using BUUME.Application.Abstractions.Messaging;

namespace BUUME.Application.Businesses.GetBusinessHeaderForCurrentUser;

public sealed record GetBusinessHeaderForCurrentUserQuery() : IQuery<BusinessHeaderResponse>;