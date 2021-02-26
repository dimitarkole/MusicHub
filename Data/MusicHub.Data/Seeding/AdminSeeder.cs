namespace MusicHub.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using MusicHub.Data.Models;

    using static MusicHub.Common.GlobalConstants;

    public class AdminSeeder : ISeeder
	{
		public const string Username = "rootadmin";
		public const string Password = "rootpass";

		public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
		{
			UserManager<User> userManager = serviceProvider.GetRequiredService<UserManager<User>>();
			User userFromDb = await userManager.FindByNameAsync(Username);

			if (userFromDb != null)
            {
				return;
			}

			var user = new User
			{
				UserName = Username,
				FirstName = "Root",
				LastName = "Root",
				Email = "root@gmail.com",
				EmailConfirmed = true,
			};

			await userManager.CreateAsync(user, Password);
			await userManager.AddToRoleAsync(user, Roles.SuperAdministrator);
		}
    }
}
