namespace BUUME.Application.Districts;

public static class DistrictErrorCodes
{
    public static class CreateDistrict
    {
        public const string MissingName = nameof(MissingName);
        public const string InvalidName = nameof(InvalidName);
        public const string MissingCode = nameof(MissingCode);
        public const string MissingCityId = nameof(MissingCityId);
    }
}