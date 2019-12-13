using AgroPi.Dal.Entities;
using AgroPi.Dal.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroPi.Dal.Seed
{
    public class RolesAndAdministratorSeeder
    {
        public UserManager<User> UserManager { get; }
        public RoleManager<Role> RoleManager { get; }

        public RolesAndAdministratorSeeder(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        public async Task Seed()
        {
            // Add Roles
            if (!await RoleManager.RoleExistsAsync(Roles.Administrators))
            {
                await RoleManager.CreateAsync(new Role { Name = Roles.Administrators });
            }
            if (!await RoleManager.RoleExistsAsync(Roles.Customer))
            {
                await RoleManager.CreateAsync(new Role { Name = Roles.Customer });
            }


            //Add Users
            if (!(await UserManager.GetUsersInRoleAsync(Roles.Administrators)).Any())
            {
                var user1 = new User
                {
                    Id = 1,
                    Email = "admin@admin.hu",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "admin@admin.hu",
                    EmailConfirmed = true
                };
                var user2 = new User
                {
                    Id = 2,
                    Email = "customer@customer.hu",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "customer@customer.hu",
                    EmailConfirmed = true
                };

                var createResult1 = await UserManager.CreateAsync(user1, "Admin12345");
                var createResult2 = await UserManager.CreateAsync(user2, "Customer12345");

                var addToRoleResult1 = await UserManager.AddToRoleAsync(user1, Roles.Administrators);
                var addToRoleResult2 = await UserManager.AddToRoleAsync(user2, Roles.Customer);

                if (!createResult1.Succeeded || !addToRoleResult1.Succeeded || !createResult2.Succeeded || !addToRoleResult2.Succeeded)
                    throw new ApplicationException($"Administrators could not created: {string.Join(", ", createResult1.Errors.Concat(addToRoleResult1.Errors).Select(e => e.Description))}");
            }
        }
    }
}
