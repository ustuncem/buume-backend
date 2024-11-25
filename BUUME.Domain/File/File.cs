using BUUME.Domain.Abstractions;

namespace BUUME.Domain.File;

public sealed class File : Entity
{
    private File(Guid id, Name name, Path path, FileType type, Checksum checksum, Size size) : base(id)
    {
        Name = name;
        Path = path;
        Type = type;
        Checksum = checksum;
        Size = size;
    }
    
    public Name Name { get; private set; }
    public Path Path { get; private set; }
    public FileType Type { get; private set; }
    public Checksum Checksum { get; private set; }
    public Size Size { get; private set; }

    public static Result<File> Create(string base64Content, string fileName, string filePath, string fileType)
    {
        var fileContent = Convert.FromBase64String(base64Content);
        
        var checksum = Checksum.FromBytes(fileContent);
        var size = new Size(fileContent.Length);
        var name = new Name(fileName);
        var path = new Path(filePath);
        var type = FileType.Create(fileType);
        
        var file = new File(Guid.NewGuid(), name, path, type, checksum, size);
        
        return Result.Create(file);
    }
}