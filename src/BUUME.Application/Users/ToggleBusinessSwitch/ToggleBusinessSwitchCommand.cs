using BUUME.Application.Abstractions.Messaging;

namespace BUUME.Application.Users.ToggleBusinessSwitch;

public sealed record ToggleBusinessSwitchCommand() : ICommand<bool>;