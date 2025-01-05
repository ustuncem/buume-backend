namespace BUUME.Infrastructure.SMS;

public sealed class SmsOptions
{
    public string Url { get; set; } = string.Empty;
    public string Key { get; set; } = string.Empty;
    public string Hash { get; set; } = string.Empty;
    public string SenderId { get; set; } = string.Empty;
    public string Iys { get; set; } = string.Empty;
    public string IysList { get; set; } = string.Empty;
}