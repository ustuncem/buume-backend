namespace BUUME.Domain.CampaignTypes;

public record Code
{
    public static readonly Code Empty = new("");
    public static readonly Code BUY_X_GET_Y = new("BUY_X_GET_Y");
    private static readonly Code PERCENTAGE_DISCOUNT = new("PERCENTAGE_DISCOUNT");
    
    private Code(string value) => Value = value;
    
    public string Value { get; init; }

    public static Code GetFromCode(string code)
    {
        return AllowedCodes.FirstOrDefault(m => m.Value == code) ?? throw new ApplicationException($"No campaign type found for value: {code}");
    }
    
    private static readonly IReadOnlyCollection<Code> AllowedCodes = [PERCENTAGE_DISCOUNT];
}