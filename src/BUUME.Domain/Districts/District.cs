using BUUME.SharedKernel;

namespace BUUME.Domain.Districts;

public sealed class District(Guid id, Name name, Guid cityId) : Entity(id)
{
    public Name Name { get; private set; } = name;
    public Guid CityId { get; private set; } = cityId;
}