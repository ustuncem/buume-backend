using System.Security.Cryptography;

namespace BUUME.Domain.File;

public record Checksum
{
    public string Value { get; init; }

    private Checksum(string value)
    {
        Value = value;
    }
    
    public static Checksum FromBytes(byte[] data)
    {
        if (data == null || data.Length == 0)
            throw new ArgumentException("Data cannot be null or empty.", nameof(data));

        using var sha256 = SHA256.Create();
        var hash = sha256.ComputeHash(data);
        var hashString = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();

        return new Checksum(hashString);
    }
}