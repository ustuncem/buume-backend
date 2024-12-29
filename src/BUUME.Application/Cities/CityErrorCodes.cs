namespace BUUME.Application.Cities;

public static class CityErrorCodes
{
    public static class CreateCity
    {
        public const string MissingName = nameof(MissingName);
        public const string InvalidName = nameof(InvalidName);
        public const string MissingCode = nameof(MissingCode);
        public const string MissingCountryId = nameof(MissingCountryId);
    }
}