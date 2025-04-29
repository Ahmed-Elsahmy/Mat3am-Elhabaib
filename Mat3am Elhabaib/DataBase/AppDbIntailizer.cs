using Mat3am_Elhabaib.Models;
using Microsoft.AspNetCore.Identity;

namespace Mat3am_Elhabaib.DataBase
{
    public class AppDbIntailizer
    {
        public static void seed(IApplicationBuilder app)
        {
            using (var servicesscope = app.ApplicationServices.CreateScope())
            {
                var context = servicesscope.ServiceProvider.GetService<AppDbContext>();
                context.Database.EnsureCreated();


            }

        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {

                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
                string adminUserEmail = "admin@medo.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new User()
                    {
                        FullName = "Admin User",
                        UserName = "admin-user",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        PhoneNumber = "01004542726",
                        Location = "Dammita"
                    };
                    await userManager.CreateAsync(newAdminUser, "Medo@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }


                string appUserEmail = "user@medo.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new User()
                    {
                        FullName = "Application User",
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        PhoneNumber = "01004542726",
                        Location = "Dammita"
                    };
                    await userManager.CreateAsync(newAppUser, "Medo@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}

