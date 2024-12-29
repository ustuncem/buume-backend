using BUUME.SharedKernel;

namespace BUUME.Domain.Cities;

public sealed class City(Guid id, Name name, Code code, Guid countryId, Guid? regionId)
    : Entity(id)
{
    public Name Name { get; private set; } = name;
    public Code Code { get; private set; } = code;
    public Guid CountryId { get; private set; } = countryId;
    public Guid? RegionId { get; private set; } = regionId;

    public City Update(Name name, Code code, Guid countryId, Guid? regionId)
    {
        Name = name;
        Code = code;
        CountryId = countryId;
        RegionId = regionId;

        return this;
    }
}