using BUUME.Domain.Countries.Events;
using BUUME.SharedKernel;

namespace BUUME.Domain.Countries;

public sealed class Country : Entity
{
    private Country(Guid id, Name name, Code code, HasRegion hasRegion) : base(id)
    {
        Name = name;
        Code = code;
        HasRegion = hasRegion;
    }
    public Name Name { get; private set; }
    public Code Code { get; private set; }
    public HasRegion HasRegion { get; private set; }

    public static Country Create(Name name, Code code, HasRegion hasRegion)
    {
        var country = new Country(Guid.NewGuid(), name, code, hasRegion);
        country.RaiseDomainEvent(new CountryCreatedDomainEvent(country.Id));
        return country;
    }

    public void Update(Name name, Code code)
    {
        Name = name;
        Code = code;
        UpdatedAt = DateTime.UtcNow;
    }
}