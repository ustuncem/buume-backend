using BUUME.SharedKernel;

namespace BUUME.Domain.Cities;

public sealed class City(Guid id, Name name, Guid countryId, Guid regionId)
    : Entity(id)
{
    public Name Name { get; private set; } = name;
    public Guid CountryId { get; private set; } = countryId;
    public Guid? RegionId { get; private set; } = regionId;
}