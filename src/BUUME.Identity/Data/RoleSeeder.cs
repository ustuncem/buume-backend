using BUUME.SharedKernel;
using Microsoft.AspNetCore.Identity;

namespace BUUME.Identity.Data;

public class RoleSeeder
{
    public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        var roles = new List<string>
        {
            Roles.SuperAdmin,
            Roles.FieldAgent,
            Roles.User
        };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}
