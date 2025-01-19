using BUUME.Application.Abstractions.Messaging;

namespace BUUME.Application.Businesses.CreateBusiness;

public record CreateBusinessCommand(
    Guid CountryId,
    Guid CityId,
    Guid DistrictId,
    string Logo,
    string TaxDocument,
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
    string[]? BusinessPhotos = null        
    ) : ICommand<Guid>;