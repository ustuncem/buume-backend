using BUUME.SharedKernel;

namespace BUUME.Domain.BusinessCategories;

public sealed class BusinessCategory(Guid id, Name name) : Entity(id)
{
    public Name Name { get; private set; } = name;
    
    public BusinessCategory Update(Name name)
    {
        UpdatedAt = DateTime.UtcNow;
        Name = name;
        return this;
    }
}