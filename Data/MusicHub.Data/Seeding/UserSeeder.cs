using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MusicHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MusicHub.Common.GlobalConstants;

namespace MusicHub.Data.Seeding
{
    public class UserSeeder : ISeeder
    {
        public class UserObj
        {
            public string Username { get; set; }

            public string Email { get; set; }

            public string Password { get; set; }

            public string Role { get; set; }
        }

        public List<UserObj> Users = new List<UserObj>()
        {
            new UserObj() { Username = "rootadmin", Email = "rootadmin@abv.bg",  Password = "rootpass", Role = Roles.SuperAdministrator },
            new UserObj() { Username = "rootadmin2", Email = "rootadmin2@abv.bg",  Password = "rootpass", Role = Roles.Administrator },
            new UserObj() { Username = "rootadmin3", Email = "rootadmin2@abv.bg",  Password = "rootpass", Role = Roles.DataAnalisator },
            new UserObj() { Username = "rootuser", Email = "rootuser1@abv.bg", Password = "rootpass", Role = Roles.Artist },
            new UserObj() { Username = "rootuser1", Email = "rootuser1@abv.bg", Password = "rootpass", Role = Roles.TeamAdministrator },
            new UserObj() { Username = "rootuser2", Email = "rootuser2@abv.bg", Password = "rootpass", Role = Roles.User },
            new UserObj() { Username = "pesho", Email = "pesho@abv.bg", Password = "rootpass", Role = Roles.Artist },
            new UserObj() { Username = "gosho", Email = "gosho@abv.bg", Password = "rootpass", Role = Roles.User },
        };

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            foreach (var user in this.Users)
            {
                var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
                var userFromDb = await userManager.FindByNameAsync(user.Username);

                if (userFromDb == null)
                {
                    var userAdd = new User
                    {
                        UserName = user.Username,
                        FirstName = "Root",
                        LastName = "Root",
                        Email = user.Email,
                        EmailConfirmed = true,
                    };

                    await userManager.CreateAsync(userAdd, user.Password);
                    await userManager.AddToRoleAsync(userAdd, user.Role);
                }
            }
        }
    }
}
