namespace BUUME.Domain.CampaignTypes;

public record Code
{
    public static readonly Code Empty = new Code("");
    public static readonly Code BUY_X_GET_Y = new Code("BUY_X_GET_Y");
    public static readonly Code PERCENTAGE_DISCOUNT = new Code("PERCENTAGE_DISCOUNT");
    
    private Code(string Value) => Value = Value;
    
    public string Value { get; init; }

    public static Code GetFromCode(string code)
    {
        return AllowedCodes.FirstOrDefault(m => m.Value == code) ?? throw new ApplicationException($"No media type found for value: {code}");
    }
    
    private static readonly IReadOnlyCollection<Code> AllowedCodes = [PERCENTAGE_DISCOUNT];
}