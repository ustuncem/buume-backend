using BUUME.Application.Abstractions.Messaging;

namespace BUUME.Application.Users.UpdateMe;

public record UpdateMeCommand(
    string? FirstName, 
    string? LastName,
    string? Email,
    int? Gender,
    string? ProfilePhoto = null) : ICommand<bool>;