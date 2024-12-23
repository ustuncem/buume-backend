namespace BUUME.Application.TaxOffices;

public static class TaxOfficeErrorCodes
{
    public static class CreateTaxOffice
    {
        public const string MissingName = nameof(MissingName);
        public const string InvalidName = nameof(InvalidName);
    }
}