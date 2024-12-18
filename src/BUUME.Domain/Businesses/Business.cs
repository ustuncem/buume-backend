using BUUME.Domain.Businesses.Events;
using BUUME.SharedKernel;

namespace BUUME.Domain.Businesses;

public sealed class Business : Entity
{
    private Business(Guid id, Name name, Guid logoId, AddressInfo addressInfo, TaxInfo taxInfo, Guid ownerId,
        IsKvkkApproved isKvkkApproved) : base(id)
    {
        Name = name;
        LogoId = logoId;
        AddressInfo = addressInfo;
        TaxInfo = taxInfo;
        OwnerId = ownerId;
        IsKvkkApproved = isKvkkApproved;
    }
    
    public Name Name { get; private set; }
    public Guid LogoId { get; private set; }
    public AddressInfo AddressInfo { get; private set; }
    public TaxInfo TaxInfo { get; private set; }
    public Guid OwnerId { get; private set; }
    public IsKvkkApproved IsKvkkApproved { get; private set; }

    public static Business Create(Name name, Guid logoId, AddressInfo addressInfo, TaxInfo taxInfo, Guid ownerId,
        IsKvkkApproved isKvkkApproved)
    {
        var business = new Business(Guid.NewGuid(), name, logoId, addressInfo, taxInfo, ownerId, isKvkkApproved);
        
        business.RaiseDomainEvent(new BusinessCreatedDomainEvent(business.Id));
        
        return business;
    }

    public void Update(Name name, Guid logoId, AddressInfo addressInfo, TaxInfo taxInfo)
    {
        Name = name;
        LogoId = logoId;
        AddressInfo = addressInfo;
        TaxInfo = taxInfo;
        
        RaiseDomainEvent(new BusinessUpdatedDomainEvent(Id));
    }
}