using BUUME.Domain.Abstractions;

namespace BUUME.Domain.File;

public sealed class Media : Entity
{
    private Media(Guid id, Name name, Path path, MediaType type, Checksum checksum, Size size) : base(id)
    {
        Name = name;
        Path = path;
        Type = type;
        Checksum = checksum;
        Size = size;
    }
    
    public Name Name { get; private set; }
    public Path Path { get; private set; }
    public MediaType Type { get; private set; }
    public Checksum Checksum { get; private set; }
    public Size Size { get; private set; }

    public static Media Create()
    {
        
    }
}