namespace BUUME.Application.Businesses;

public static class BusinessErrorCodes
{
    public const string MissingLogo = nameof(MissingLogo);
    public const string MissingTaxDocument = nameof(MissingTaxDocument);
    public const string MissingCountry = nameof(MissingCountry);
    public const string MissingCity = nameof(MissingCity);
    public const string MissingDistrict = nameof(MissingDistrict);
    public const string MissingName = nameof(MissingName);
    public const string InvalidName = nameof(InvalidName);
    public const string InvalidEmail = nameof(InvalidEmail);
    public const string MissingPhone = "Lütfen telefon numarası ekle.";
    public const string InvalidPhone = "Lütfen doğru formatta bir telefon numarası gir.";
    public const string MissingAddress = nameof(MissingAddress);
    public const string MissingLatitude = nameof(MissingLatitude);
    public const string MissingLongitude = nameof(MissingLongitude);
    public const string MissingKvkk = nameof(MissingKvkk);
}