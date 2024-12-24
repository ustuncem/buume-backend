namespace BUUME.Application.Countries;

public static class CountryErrorCodes
{
    public static class CreateCountry
    {
        public const string MissingName = nameof(MissingName);
        public const string InvalidName = nameof(InvalidName);
    }
}