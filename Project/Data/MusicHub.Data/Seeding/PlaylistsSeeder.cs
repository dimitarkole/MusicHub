namespace MusicHub.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Data.Models;

    using static MusicHub.Common.GlobalConstants;

    public class PlaylistsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var playlists = this.GetSongsPlaylists();
            var userRoleId = dbContext.Roles
                .Where(r => r.Name == Roles.UserRoleName)
                .FirstOrDefault()
                .Id;
            var users = dbContext.Users
                  .Where(u => u.Roles.Any(r => r.RoleId == userRoleId))
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
                    },
                    new Playlist()
                    {
                        Name = "Summer",
                    },
                    new Playlist()
                    {
                        Name = "The best hits",
                    },
                    new Playlist()
                    {
                        Name = "Home musics",
                    },
                    new Playlist()
                    {
                        Name = "Good morning",
                    },
                    new Playlist()
                    {
                        Name = "Happy minutes",
                    },
                };
    }
}
