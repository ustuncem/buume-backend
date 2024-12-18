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
}