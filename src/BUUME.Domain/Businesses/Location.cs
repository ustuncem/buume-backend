namespace BUUME.Domain.Businesses;

public sealed record Location
{
    private static readonly int[] LatRange = [-90, 90];
    private static readonly int[] LongRange = [-180, 180];
    
    public double Latitude { get; init; }
    public double Longitude { get; init; }

    private Location(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }
    public static Location Create(double latitude, double longitude)
    {
        if (latitude < LatRange[0] || latitude > LatRange[1])
            throw new ArgumentOutOfRangeException(nameof(latitude), "Latitude must be between -90 and 90.");
        if (longitude < LongRange[0] || longitude > LongRange[1])
            throw new ArgumentOutOfRangeException(nameof(longitude), "Longitude must be between -180 and 180.");
        
        var location = new Location(latitude, longitude);
        return location;
    }
}