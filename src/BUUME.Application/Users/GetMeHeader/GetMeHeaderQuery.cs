using BUUME.Application.Abstractions.Messaging;

namespace BUUME.Application.Users.GetMeHeader;

public sealed record GetMeHeaderQuery() : IQuery<MeHeaderResponse>;