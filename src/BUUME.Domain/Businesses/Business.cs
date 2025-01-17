using BUUME.Domain.Businesses.Events;
using BUUME.SharedKernel;

namespace BUUME.Domain.Businesses;

public sealed class Business : Entity
{
    private Business(
        Guid id,
        Guid logoId,
        Guid ownerId,
        Guid countryId,
        Guid cityId,
        Guid districtId,
        BaseInfo baseInfo,
        Address address, 
        Location location,
        IsKvkkApproved isKvkkApproved, 
        WorkingHours? workingHours = null) : base(id)
    {
        LogoId = logoId;
        OwnerId = ownerId;
        CountryId = countryId;
        CityId = cityId;
        DistrictId = districtId;
        BaseInfo = baseInfo;
        Address = address;
        Location = location;
        IsKvkkApproved = isKvkkApproved;
        WorkingHours = workingHours;
    }
    
    private Business() {}
    
    public Guid LogoId { get; private set; }
    public Guid OwnerId { get; private set; }
    public Guid CountryId { get; private set; }
    public Guid CityId { get; private set; }
    public Guid DistrictId { get; private set; }
    public BaseInfo BaseInfo { get; private set; }
    public Address Address { get; private set; }
    public Location Location { get; private set; }
    public IsKvkkApproved IsKvkkApproved { get; private set; }
    public WorkingHours? WorkingHours { get; private set; }

    public static Business Create(
        Guid logoId,
        Guid ownerId,
        Guid countryId,
        Guid cityId,
        Guid districtId,
        BaseInfo baseInfo,
        Address address, 
        Location location,
        IsKvkkApproved isKvkkApproved, 
        WorkingHours? workingHours = null
        )
    {
        var business = new Business(
            Guid.NewGuid(), 
            logoId,
            ownerId,
            countryId,
            cityId,
            districtId,
            baseInfo,
            address,
            location,
            isKvkkApproved,
            workingHours);
        
        business.RaiseDomainEvent(new BusinessCreatedDomainEvent(business.Id));
        
        return business;
    }

    public void Update(
        Guid logoId,
        Guid ownerId,
        Guid countryId,
        Guid cityId,
        Guid districtId,
        BaseInfo baseInfo,
        Address address,
        Location location,
        IsKvkkApproved isKvkkApproved, 
        WorkingHours? workingHours = null)
    {
        LogoId = logoId;
        OwnerId = ownerId;
        CountryId = countryId;
        CityId = cityId;
        DistrictId = districtId;
        BaseInfo = baseInfo;
        Address = address;
        Location = location;
        IsKvkkApproved = isKvkkApproved;
        WorkingHours = workingHours;
        UpdatedAt = DateTime.UtcNow;
        
        RaiseDomainEvent(new BusinessUpdatedDomainEvent(Id));
    }
}