namespace MusicHub.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    using static MusicHub.Common.GlobalConstants;

    public class ApplicationUsersSeeder : ISeeder
    {
        public class User
        {
            public string Username { get; set; }

            public string Email { get; set; }

            public string Password { get; set; }

            public string Role { get; set; }
        }

        public List<User> Users = new List<User>()
        {
            new User() { Username = "rootadmin", Email = "rootadmin@abv.bg",  Password = "rootpass", Role = Roles.AdministratorRoleName },
            new User() { Username = "rootuser", Email = "rootuser@abv.bg", Password = "rootpass", Role = Roles.UserRoleName },
            new User() { Username = "rootuser1", Email = "rootuser1@abv.bg", Password = "rootpass", Role = Roles.UserRoleName },
            new User() { Username = "rootuser2", Email = "rootuser2@abv.bg", Password = "rootpass", Role = Roles.UserRoleName },
            new User() { Username = "pesho", Email = "pesho@abv.bg", Password = "rootpass", Role = Roles.UserRoleName },
            new User() { Username = "gosho", Email = "gosho@abv.bg", Password = "rootpass", Role = Roles.UserRoleName },
        };

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            foreach (var user in this.Users)
            {
                var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var userFromDb = await userManager.FindByNameAsync(user.Username);

                if (userFromDb == null)
                {
                    var userAdd = new ApplicationUser
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
