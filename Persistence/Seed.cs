using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context,
     UserManager<AppUser> userManager,
     RoleManager<IdentityRole> roleManager)
        {
            await SeedRoles(roleManager);
            var admin = await SeedUsers(userManager);
            await SeedApps(context, admin);
        }

        public static async Task<AppUser> SeedUsers(UserManager<AppUser> userManager)
        {
            if (await userManager.FindByNameAsync
("johndoe") == null)
            {
                var user = new AppUser();
                user.UserName = "johndoe";
                user.Email = "johndoe@localhost.com";
                user.DisplayName = "John Doe";
                IdentityResult result = userManager.CreateAsync
                (user, "P@ssw0rd!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,
                                        "Member").Wait();
                }
            }
            if (await userManager.FindByNameAsync
("janedoe") == null)
            {
                var user = new AppUser();
                user.UserName = "janedoe";
                user.Email = "janedoe@localhost.com";
                user.DisplayName = "jane Doe";

                IdentityResult result = userManager.CreateAsync
                (user, "P@ssw0rd!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,
                                        "member").Wait();
                }
            }
            if (await userManager.FindByNameAsync
("admin") == null)
            {
                var user = new AppUser();
                user.UserName = "admin";
                user.Email = "bahadirdoser@localhost.com";
                user.DisplayName = "Bahadir Doser";

                IdentityResult result = userManager.CreateAsync
                (user, "P@ssw0rd!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,
                                        "administrator").Wait();
                }
            }
            return userManager.FindByNameAsync
            ("admin").Result;
        }

        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync
                ("administrator").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "administrator";
                IdentityResult roleResult = await roleManager.
                CreateAsync(role);
            }
            if (!roleManager.RoleExistsAsync
                ("Member").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "member";
                IdentityResult roleResult = await roleManager.
                CreateAsync(role);
            }
        }
        public static async Task<List<TargetApp>> SeedApps(DataContext context, AppUser admin)
        {
            if (!context.TargetApps.Any())
            {
                var targetApps = new List<TargetApp>{
                    new TargetApp
                    {
                        Name = "Eksi Sozluk",
                        Interval = 1,
                        IsActive = false,
                        Url = "https://eksisozluk.com/",
                        CreatedById = admin.Id,
                         Created = DateTime.UtcNow
                    },
                    new TargetApp
                    {
                        Name = "DOTA",
                        Interval = 1,
                        IsActive = false,
                        Url = "https://steamdb.info/api/GetGraph/?type=concurrent_week&appid=570",
                        CreatedById = admin.Id,
                         Created = DateTime.UtcNow
                    },
                };
                await context.TargetApps.AddRangeAsync(targetApps);
                await context.SaveChangesAsync();
                // return context.Genres.ToList();
                return targetApps;
            }
            return null;
        }
    }
}