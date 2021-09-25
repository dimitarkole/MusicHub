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

    public class PlaylistsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var playlists = this.GetSongsPlaylists();
            var userRoleId = dbContext.Roles
                .Where(r => r.Name == Roles.User)
                .FirstOrDefault()
                .Id;
            var users = dbContext.Users
                  .Where(u => u.Roles.Any(r => r.Role.Permissions.Any(p => p.Permission.Type == PermissionType.PlaylistCreator)))
                  .ToArray();
            foreach (var playlist in playlists)
            {
                var random = new Random();
                var userId = users[random.Next(0, users.Count())].Id;
                if (!dbContext.Playlists.Any(e => e.Name == playlist.Name))
                {
                    playlist.UserId = userId;
                    await dbContext.Playlists.AddAsync(playlist);
                }
            }
        }

        private Playlist[] GetSongsPlaylists()
            => new Playlist[]
                {
                    new Playlist()
                    {
                        Name = "Happy music",
                        Privacy = Privacy.Protected,
                    },
                    new Playlist()
                    {
                        Name = "Summer",
                        Privacy = Privacy.Protected,
                    },
                    new Playlist()
                    {
                        Name = "The best hits",
                        Privacy = Privacy.Public,
                    },
                    new Playlist()
                    {
                        Name = "Home musics",
                        Privacy = Privacy.Public,
                    },
                    new Playlist()
                    {
                        Name = "Good morning",
                        Privacy = Privacy.Public,
                    },
                    new Playlist()
                    {
                        Name = "Happy minutes",
                        Privacy = Privacy.Public,
                    },
                    new Playlist()
                    {
                        Name = "Soft music",
                        Privacy = Privacy.Public,
                    },
                    new Playlist()
                    {
                        Name = "Phone music",
                        Privacy = Privacy.Private,
                    }, new Playlist()
                    {
                        Name = "Love moment",
                        Privacy = Privacy.Private,
                    }, new Playlist()
                    {
                        Name = "R&B",
                        Privacy = Privacy.Public,
                    }, new Playlist()
                    {
                        Name = "Jazz",
                        Privacy = Privacy.Protected,
                    }, new Playlist()
                    {
                        Name = "Hip hop",
                        Privacy = Privacy.Public,
                    }, new Playlist()
                    {
                        Name = "Love minutes",
                        Privacy = Privacy.Public,
                    },
                };
    }
}
