using BUUME.Domain.Abstractions;
using BUUME.Domain.Roles.Events;

namespace BUUME.Domain.Roles;

public sealed class Role : Entity
{
    private Role(Guid id, Name name, Description description) : base(id)
    {
        Name = name;
        Description = description;
    }
    
    public Name Name { get; private set; }
    public Description Description { get; private set; }

    public static Role Create(Name name, Description description)
    {
        var role = new Role(Guid.NewGuid(), name, description);
        
        role.RaiseDomainEvent(new RoleCreatedDomainEvent(role.Id));

        return role;
    }
    
    public void UpdateDescription(Description newDescription)
    {
        Description = newDescription;
    }
}
