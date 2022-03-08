namespace MusicHub.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;

    using static MusicHub.Common.GlobalConstants;

    public class AlbumsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var albums = this.GetAlbums();
            var userRoleId = dbContext.Roles
                .Where(r => r.Name == Roles.User)
                .FirstOrDefault()
                .Id;
            var users = dbContext.Users
                  .Where(u => u.Roles.Any(r => r.Role.Permissions.Any(p => p.Permission.Type == PermissionType.AlbumCreator)))
                  .ToArray();
            foreach (var album in albums)
            {
                var random = new Random();
                var userId = users[random.Next(0, users.Count())].Id;
                if (!dbContext.Albums.Any(e => e.Name == album.Name))
                {
                    album.UserId = userId;
                    await dbContext.Albums.AddAsync(album);
                }
            }
        }

        private Album[] GetAlbums()
            => new Album[]
                {
                    new Album()
                    {
                        Name = "Happy music",
                        Privacy = Privacy.Protected,
                    },
                    new Album()
                    {
                        Name = "Summer",
                        Privacy = Privacy.Protected,
                    },
                    new Album()
                    {
                        Name = "The best hits",
                        Privacy = Privacy.Public,
                    },
                    new Album()
                    {
                        Name = "Home musics",
                        Privacy = Privacy.Public,
                    },
                    new Album()
                    {
                        Name = "Good morning",
                        Privacy = Privacy.Public,
                    },
                    new Album()
                    {
                        Name = "Happy minutes",
                        Privacy = Privacy.Public,
                    },
                    new Album()
                    {
                        Name = "Soft music",
                        Privacy = Privacy.Public,
                    },
                    new Album()
                    {
                        Name = "Phone music",
                        Privacy = Privacy.Private,
                    },
                    new Album()
                    {
                        Name = "Love moment",
                        Privacy = Privacy.Private,
                    },
                    new Album()
                    {
                        Name = "R&B",
                        Privacy = Privacy.Public,
                    },
                    new Album()
                    {
                        Name = "Jazz",
                        Privacy = Privacy.Protected,
                    },
                    new Album()
                    {
                        Name = "Hip hop",
                        Privacy = Privacy.Public,
                    },
                    new Album()
                    {
                        Name = "Love minutes",
                        Privacy = Privacy.Public,
                    },
                };
    }
}
