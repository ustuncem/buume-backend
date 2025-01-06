using BUUME.Application.Abstractions.Messaging;

namespace BUUME.Application.Users.UpdateMe;

public record UpdateMeCommand(
    string? FirstName, 
    string? LastName,
    string? Email,
    string PhoneNumber,
    string? BirthDate,
    int? Gender) : ICommand<bool>;