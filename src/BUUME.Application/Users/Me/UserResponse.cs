namespace BUUME.Application.Users.Me;

public sealed class UserResponse
{
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? Email { get; init; }
    public string? ProfilePicture { get; init; }
    public string PhoneNumber { get; init; } = default!;
    public bool? IsPhoneNumberVerified { get; init; }
    public DateTime? BirthDate { get; init; }
    public int? Gender { get; init; }
}