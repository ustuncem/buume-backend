namespace BUUME.Application.Businesses.GetBusinessForCurrentUser;

public record CountryResponse(Guid CountryId, string CountryName);
public record CityResponse(Guid CityId, string CityName);
public record DistrictResponse(Guid DistrictId, string DistrictName);
public record BusinessCategoryResponse(Guid BusinessCategoryId, string BusinessCategoryName);

public record BusinessFileResponse(Guid BusinessFileId, string BusinessFilePath);

public class BusinessResponse
{
    public Guid BusinessId { get; set; }
    public CountryResponse Country { get; set; }
    public CityResponse City { get; set; }
    public DistrictResponse District { get; set; }
    public Guid LogoId { get; set; }
    public string Logo { get; set; }
    public string TaxDocument { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public bool IsKvkkApproved { get; set; }
    public string? StartTime { get; set; }
    public string? EndTime { get; set; }
    public string? Description { get; set; }
    public string? OnlineOrderLink { get; set; }
    public string? MenuLink { get; set; }
    public string? WebsiteLink { get; set; }
    public BusinessCategoryResponse[] BusinessCategories { get; set; }
    public BusinessFileResponse[]? BusinessPhotos { get; set; }
}