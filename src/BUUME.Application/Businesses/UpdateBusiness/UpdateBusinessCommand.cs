using BUUME.Application.Abstractions.Messaging;

namespace BUUME.Application.Businesses.UpdateBusiness;

public record UpdateBusinessCommand(
    Guid CountryId,
    Guid CityId,
    Guid DistrictId,
    string Name, 
    string Email, 
    string PhoneNumber,
    string Address,
    double Latitude, 
    double Longitude,
    bool IsKvkkApproved,
    string? StartTime,
    string? EndTime,
    string? Description = null, 
    string? OnlineOrderLink = null, 
    string? MenuLink = null, 
    string? WebsiteLink = null,
    Guid[]? BusinessCategoryIds = null,
    string[]? BusinessPhotos = null,    
    string? Logo = null
    ) : ICommand<Guid>;