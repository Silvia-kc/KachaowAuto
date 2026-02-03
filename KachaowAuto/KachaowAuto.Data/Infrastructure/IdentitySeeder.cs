using KachaowAuto.Common;
using KachaowAuto.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Data.Infrastructure
{
    public static class IdentitySeeder
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole<int>> roleManager)
        {
            string[] roles = { "Admin", "Mechanic", "Client" };

            foreach (var role in roles)
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole<int>(role));
        }

        public static async Task SeedAdminAsync(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole<int>> roleManager)
        {
            const string adminEmail = "admin@kachaowauto.com";
            const string adminPassword = "Admin123!"; 

            await SeedRolesAsync(roleManager);

            var admin = await userManager.FindByEmailAsync(adminEmail);
            if (admin == null)
            {
                admin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FullName = "System Admin",     
                    CreatedAt = DateTime.UtcNow    
                };

                var createResult = await userManager.CreateAsync(admin, adminPassword);
                if (!createResult.Succeeded) return;
            }

            if (!await userManager.IsInRoleAsync(admin, "Admin"))
            {
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}
