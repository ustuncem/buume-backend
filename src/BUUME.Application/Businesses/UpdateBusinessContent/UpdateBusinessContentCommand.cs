using BUUME.Application.Abstractions.Messaging;

namespace BUUME.Application.Businesses.UpdateBusinessContent;

public sealed record UpdateBusinessContentCommand(Guid BusinessId, Guid FileId, string NewBase64Content, string? Mode = "logo") : ICommand<bool>;