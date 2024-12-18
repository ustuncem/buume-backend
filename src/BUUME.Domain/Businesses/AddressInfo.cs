namespace BUUME.Domain.Businesses;

public record AddressInfo(Guid CountryId, Guid CityId, Guid DistrictId, string Address, decimal Latitude, decimal Longitude);