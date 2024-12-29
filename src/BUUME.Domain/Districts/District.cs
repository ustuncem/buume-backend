using BUUME.SharedKernel;

namespace BUUME.Domain.Districts;

public sealed class District(Guid id, Name name, Code code, Guid cityId) : Entity(id)
{
    public Name Name { get; private set; } = name;
    public Code Code { get; private set; } = code;
    public Guid CityId { get; private set; } = cityId;

    public District UpdateName(Name name, Guid cityId)
    {
        Name = name;
        CityId = cityId;
        UpdatedAt = DateTime.UtcNow;
        
        return this;
    }
}