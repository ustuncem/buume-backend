namespace BUUME.Application.Users.GetMeHeader;

public sealed class MeHeaderResponse
{
    public string FirstName { get; init; } = default!;
    public string LastName { get; init; } = default!;
    public string ProfilePhoto { get; init; } = default!;
}