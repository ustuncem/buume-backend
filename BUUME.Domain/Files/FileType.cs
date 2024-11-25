using BUUME.Domain.Abstractions;

namespace BUUME.Domain.Files;

public record FileType
{
    public static readonly FileType Png = new("image/png");
    public static readonly FileType Jpeg = new("image/jpeg");
    public static readonly FileType Webp = new("image/webp");
    
    public string Value { get; init; }
    
    private FileType(string value) => Value = value;

    public static FileType Create(string fileType)
    {
        return All.FirstOrDefault(m => m.Value == fileType) ?? throw new ApplicationException($"No media type found for value: {fileType}");
    }
    
    private static readonly IReadOnlyCollection<FileType> All =
    [
        Png,
        Jpeg,
        Webp
    ];
}