using Lab_System.Models;
using Microsoft.AspNetCore.Identity;

namespace Lab_System.Data.Seed
{
    public static class RoleSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roles =
            {
                "Admin",
                "LabManager",
                "Supervisor",
                "Researcher"
            };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            string email = "admin@labsystem.com";
            string password = "Admin@399";

            var admin = await userManager.FindByEmailAsync(email);

            if (admin == null)
            {
                admin = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    FullName = "System Admin",
                    RoleName = "Admin",
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(admin, password);
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}
