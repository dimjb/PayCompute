﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PayCompute.Persistence
{
    public static class DataSeedingInitializer
    {
        public static async Task UserAndRoleSeedAsync(UserManager<IdentityUser> userManager,
                                                 RoleManager<IdentityRole> roleManager)
        {
            string[] roles = { "Admin", "Manager", "Staff" };
            foreach (var role in roles)
            {
                var roleExist = await roleManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    IdentityResult result = await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            //Create Admin User
            if (userManager.FindByEmailAsync("admin@test.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "admin@test.com",
                    Email = "admin@test.com"
                };
                IdentityResult identityResult = userManager.CreateAsync(user, "Password1").Result;
                if (identityResult.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }

            //Create Manager User
            if (userManager.FindByEmailAsync("manager@test.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "manager@test.com",
                    Email = "manager@test.com"
                };
                IdentityResult identityResult = userManager.CreateAsync(user, "Password1").Result;
                if (identityResult.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Manager").Wait();
                }
            }

            //Create Staff User
            if (userManager.FindByEmailAsync("staff@test.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "staff@test.com",
                    Email = "staff@test.com"
                };
                IdentityResult identityResult = userManager.CreateAsync(user, "Password1").Result;
                if (identityResult.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Staff").Wait();
                }
            }

            //Create No Role User
            if (userManager.FindByEmailAsync("doe@gmail.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "doe@gmail.com",
                    Email = "doe@gmail.com"
                };
                IdentityResult identityResult = userManager.CreateAsync(user, "Password1").Result;
                //No Role assigned to Mr John Doe
            }
        }
    }

}
