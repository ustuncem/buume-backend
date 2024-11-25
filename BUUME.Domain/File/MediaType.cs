using BUUME.Domain.Abstractions;

namespace BUUME.Domain.File;

public record MediaType
{
    public static readonly MediaType Png = new("image/png");
    public static readonly MediaType Jpeg = new("image/jpeg");
    public static readonly MediaType Webp = new("image/webp");
    
    public string Value { get; init; }
    
    private MediaType(string value) => Value = value;

    public static MediaType Create(string mediaType)
    {
        return All.FirstOrDefault(m => m.Value == mediaType) ?? throw new ApplicationException($"No media type found for value: {mediaType}");
    }
    
    public static readonly IReadOnlyCollection<MediaType> All = new[]
    {
        Png,
        Jpeg,
        Webp
    };
}