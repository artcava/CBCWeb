using Microsoft.AspNetCore.Identity;

namespace CenturyBelongingCalculator.Web.Areas.Identity.Data;

public class IdentitySeedData
{
    public static async Task Initialize(
        AuthenticationDbContext context, 
        UserManager<IdentityUser> userManager, 
        RoleManager<IdentityRole> roleManager)
    {
        context.Database.EnsureCreated();
        var roles = new[] { "Admin", "Member" };
        var user = await userManager.FindByEmailAsync("cavallo.marco@gmail.com");

        if (user != null)
        {

            foreach (var role in roles)
            {
                var hasRole = await roleManager.RoleExistsAsync(role);
                if (!hasRole) await roleManager.CreateAsync(new IdentityRole(role));
                var exists = await userManager.IsInRoleAsync(user, role);
                if (!exists)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }
    }
}
