using BUUME.Domain.CampaignTypes.Events;
using BUUME.SharedKernel;

namespace BUUME.Domain.CampaignTypes;

public sealed class CampaignType : Entity
{
    private CampaignType(Guid id, Name name, Description description, Code code) : base(id)
    {
        Name = name;
        Description = description;
        Code = code;
    }
    
    public Name Name { get; private set; }
    public Description Description { get; private set; }
    public Code Code { get; private set; }
    
    public static CampaignType Create(Name name, Description description, Code code)
    {
        var guid = Guid.NewGuid();
        var campaignType = new CampaignType(guid, name, description, code);
        campaignType.RaiseDomainEvent(new CampaignTypeCreatedDomainEvent(guid));
        return campaignType;
    }

    public void Update(Name name, Description description, Code code)
    {
        Name = name;
        Description = description;
        Code = code;
        UpdatedAt = DateTime.UtcNow;
        RaiseDomainEvent(new CampaignTypeUpdatedDomainEvent(Id));
    }
}