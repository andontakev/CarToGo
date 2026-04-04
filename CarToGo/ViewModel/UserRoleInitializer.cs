using CarToGo.Controllers;
using CarToGo.Models;
using Microsoft.AspNetCore.Identity;

namespace CarToGo.ViewModel
{
    public class UserRoleInitializer
    {
        /// <summary>
        /// Initializes the application's identity system by ensuring required roles and a default administrator user
        /// exist.
        /// </summary>
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<DefaultUser>>();

            string[] roleNames = { "Admin", "User" };

            IdentityResult roleResult;

            foreach (var role in roleNames)
            {
                var roleExists = await roleManager.RoleExistsAsync(role);

                if (!roleExists)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var email = "admin@cartogo.com";
            var password = "Admin123!";

            if (userManager.FindByEmailAsync(email).Result == null)
            {
                DefaultUser user = new DefaultUser
                {
                    UserName = email,
                    Email = email,
                    FirstName = "Admin",
                    LastName = "Admin",
                    PhoneNumber = "1234567890",
                    EGN = "1234567890",
                    EmailConfirmed = true
                };
                IdentityResult result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }
    }
}
