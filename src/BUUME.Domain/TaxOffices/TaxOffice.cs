using BUUME.SharedKernel;

namespace BUUME.Domain.TaxOffices;

public sealed class TaxOffice(Guid id, Name name) : Entity(id)
{
    public Name Name { get; private set; } = name;

    public TaxOffice Update(Name name)
    {
        UpdatedAt = DateTime.UtcNow;
        Name = name;
        return this;
    }
}