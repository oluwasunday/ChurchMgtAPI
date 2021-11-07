using church_mgt_models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace church_mgt_database.seeder
{
    public static class ChurchMgtSeeder
    {
        public static async Task SeedData(ChurchDbContext dbContext, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var baseDir = Directory.GetCurrentDirectory();

            await dbContext.Database.EnsureCreatedAsync();

            if (!dbContext.Users.Any())
            {
                List<string> roles = new List<string> { "Admin", "Pastor", "SuperPastor", "Worker", "Guest", "Member" };

                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = role });
                }

                var user = new AppUser
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = "RCCG",
                    MiddleName = "Admin",
                    LastName = "Ogun5",
                    UserName = "churchadmin",
                    Email = "info@rccgogun5.com",
                    PhoneNumber = "09043546576",
                    Gender = "Male",
                    DOB = DateTime.UtcNow,
                    IsBornAgain = true,
                    Avatar = "http://placehold.it/32x32",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    PublicId = Guid.NewGuid().ToString()
                };

                await userManager.CreateAsync(user, "Password@123");
                await userManager.AddToRoleAsync(user, "Admin");

                var superPastor = new AppUser
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = "Pastor",
                    FirstName = "Abimbola",
                    MiddleName = "Joseph",
                    LastName = "Oluwasegun",
                    UserName = "abimbolajoseph-o",
                    Email = "pastor@rccgogun5.com",
                    PhoneNumber = "09043546577",
                    Gender = "Male",
                    DOB = DateTime.UtcNow,
                    IsBornAgain = true,
                    Avatar = "http://placehold.it/32x32",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    PublicId = Guid.NewGuid().ToString()
                };
                await userManager.CreateAsync(superPastor, "Password@123");
                await userManager.AddToRoleAsync(superPastor, "SuperPastor");
            }
            await dbContext.SaveChangesAsync();
        }

        static string FilePath(string folderName, string fileName)
        {
            return Path.Combine(folderName, fileName);
        }
    }
}
