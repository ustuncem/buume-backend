using BUUME.Application.Abstractions.Messaging;

namespace BUUME.Application.Businesses.CreateBusinessContent;

public sealed record CreateBusinessContentCommand(Guid BusinessId, string NewBase64Content) : ICommand<string>;