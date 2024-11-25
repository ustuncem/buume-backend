using BUUME.Domain.Abstractions;

namespace BUUME.Domain.Countries;

public sealed class Country : Entity
{
    public Country(Guid id, Name name, Code code, HasRegion hasRegion) : base(id)
    {
        Name = name;
        Code = code;
        HasRegion = hasRegion;
    }
    public Name Name { get; private set; }
    public Code Code { get; private set; }
    public HasRegion HasRegion { get; private set; }
}