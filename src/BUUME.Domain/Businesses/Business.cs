using BUUME.Domain.Businesses.Events;
using BUUME.SharedKernel;

namespace BUUME.Domain.Businesses;

public sealed class Business : Entity
{
    private Business(
        Guid id,
        Guid logoId,
        Guid taxDocumentId,
        Guid ownerId,
        Guid countryId,
        Guid cityId,
        Guid districtId,
        BaseInfo baseInfo,
        Address address, 
        Location location,
        IsKvkkApproved isKvkkApproved,
        WorkingHours? workingHours = null,
        Guid? validatorId = null) : base(id)
    {
        LogoId = logoId;
        TaxDocumentId = taxDocumentId;
        OwnerId = ownerId;
        CountryId = countryId;
        CityId = cityId;
        DistrictId = districtId;
        BaseInfo = baseInfo;
        Address = address;
        Location = location;
        IsKvkkApproved = isKvkkApproved;
        WorkingHours = workingHours;
        ValidatorId = validatorId;
        IsEnabled = new IsEnabled(false);
    }
    
    private Business() {}
    
    public Guid? LogoId { get; private set; }
    public Guid TaxDocumentId { get; private set; }
    public Guid OwnerId { get; private set; }
    public Guid CountryId { get; private set; }
    public Guid CityId { get; private set; }
    public Guid DistrictId { get; private set; }
    public BaseInfo BaseInfo { get; private set; }
    public Address Address { get; private set; }
    public Location Location { get; private set; }
    public IsKvkkApproved IsKvkkApproved { get; private set; }
    public WorkingHours? WorkingHours { get; private set; }
    public Guid? ValidatorId { get; private set; }
    public IsEnabled IsEnabled { get; private set; }

    public static Business Create(
        Guid logoId,
        Guid taxDocumentId,
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
            taxDocumentId,
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
        Guid countryId,
        Guid cityId,
        Guid districtId,
        BaseInfo baseInfo,
        Address address,
        Location location,
        IsKvkkApproved isKvkkApproved, 
        WorkingHours? workingHours = null)
    {
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

    public void UpdateLogo(Guid logoId)
    {
        LogoId = logoId;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ToggleIsEnabled(Guid validatorId)
    {
        ValidatorId = validatorId;
        IsEnabled = new IsEnabled(!IsEnabled.Value);
        UpdatedAt = DateTime.UtcNow;
    } 
}