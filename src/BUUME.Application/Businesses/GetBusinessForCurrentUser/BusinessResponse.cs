namespace BUUME.Application.Businesses.GetBusinessForCurrentUser;

public record CountryResponse(Guid CountryId, string CountryName);
public record CityResponse(Guid CityId, string CityName);
public record DistrictResponse(Guid DistrictId, string DistrictName);
public record TaxOfficeResponse(Guid TaxOfficeId, string TaxOfficeName);
public record BusinessCategoryResponse(Guid BusinessCategoryId, string BusinessCategoryName);

public class BusinessResponse
{
    public Guid BusinessId { get; set; }
    public CountryResponse Country { get; set; }
    public CityResponse City { get; set; }
    public DistrictResponse District { get; set; }
    public TaxOfficeResponse TaxOffice { get; set; }
    public string Logo { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public string TradeName { get; set; }
    public string Vkn { get; set; }
    public bool IsKvkkApproved { get; set; }
    public string? StartTime { get; set; }
    public string? EndTime { get; set; }
    public string? Description { get; set; }
    public string? OnlineOrderLink { get; set; }
    public string? MenuLink { get; set; }
    public string? WebsiteLink { get; set; }
    public BusinessCategoryResponse[] BusinessCategories { get; set; }
    public string[]? BusinessPhotos { get; set; }
}