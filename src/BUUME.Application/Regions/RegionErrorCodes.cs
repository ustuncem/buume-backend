namespace BUUME.Application.Regions;

public static class RegionErrorCodes
{
    public static class CreateRegion
    {
        public const string MissingName = nameof(MissingName);
        public const string InvalidName = nameof(InvalidName);
        public const string MissingCountryId = nameof(MissingCountryId);
    }
}