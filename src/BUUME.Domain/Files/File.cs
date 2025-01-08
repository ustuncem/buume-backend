using BUUME.SharedKernel;

namespace BUUME.Domain.Files;

public sealed class File : Entity
{
    private File(Guid id, Name name, Path path, FileType type, Size size) : base(id)
    {
        Name = name;
        Path = path;
        Type = type;
        Size = size;
    }
    
    public Name Name { get; private set; }
    public Path Path { get; private set; }
    public FileType Type { get; private set; }
    public Size Size { get; private set; }

    public static File Create(double fileSize, string fileName, string filePath, string fileType)
    {
        var size = new Size(fileSize);
        var name = new Name(fileName);
        var path = new Path(filePath);
        var type = FileType.Create(fileType);
        
        var file = new File(Guid.NewGuid(), name, path, type, size);
        
        return file;
    }
}