namespace BUUME.Application.BusinessCategories;

public static class BusinessCategoryErrorCodes
{
    public static class CreateBusinessCategory
    {
        public const string MissingName = nameof(MissingName);
        public const string InvalidName = nameof(InvalidName);
    }
}