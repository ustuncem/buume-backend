namespace BUUME.Domain.Businesses;

public record BaseInfo(
    string Name, 
    string Email, 
    string PhoneNumber, 
    string? Description = null, 
    string? OnlineOrderLink = null, 
    string? MenuLink = null, 
    string? WebsiteLink = null);