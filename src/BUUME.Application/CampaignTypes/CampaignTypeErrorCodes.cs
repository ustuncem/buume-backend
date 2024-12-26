namespace BUUME.Application.CampaignTypes;

public static class CampaignTypeErrorCodes
{
    public static class CreateCampaignType
    {
        public const string MissingName = nameof(MissingName);
        public const string InvalidName = nameof(InvalidName);
        public const string MissingCode = nameof(MissingCode);
        public const string MaxLengthCode = nameof(MaxLengthCode);
        public const string MaxLengthDescription = nameof(MaxLengthDescription);
    }
}