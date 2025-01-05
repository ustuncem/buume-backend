using Microsoft.AspNetCore.Identity;

namespace BUUME.Identity.Data;

public class ApplicationUser : IdentityUser
{
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime RefreshTokenExpiration { get; set; }

    public ApplicationUser()
    {
        
    }

    public ApplicationUser(string phoneNumber) : base(phoneNumber)
    {
        
    }
}