using BUUME.Domain.Abstractions;

namespace BUUME.Domain.Regions;

public sealed class Region : Entity
{
    private Region(Guid id, Name name, Guid countryId) : base(id)
    {
        Name = name;
        CountryId = countryId;
    }
    
    public Name Name { get; private set; }
    public Guid CountryId { get; private set; }

    public static Region Create(Name name, Guid countryId)
    {
        var region = new Region(Guid.NewGuid(), name, countryId);
        return region;
    }
    
    public void Update(Name name) => Name = name;
}