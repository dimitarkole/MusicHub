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

    public class ReportMusicSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.ReportMusics.Count() == 0)
            {
                var random = new Random(0);
                var musics = dbContext.Musics.ToArray();
                var users = dbContext.Users.Where(u => u.Roles.Any(r => r.Role.Name == Roles.User)).ToList();
                var usersCount = random.Next(1, users.Count - 1);
                Array values = Enum.GetValues(typeof(ReportMusicType));
                for (int i = 0; i < usersCount; i++)
                {
                    var randomUser = users[random.Next(0, users.Count - 1)];
                    users.Remove(randomUser);
                    var reportedMusicCount = random.Next(0, musics.Length / 3);
                    var allMusics = new List<Music>(musics);
                    for (int j = 0; j < reportedMusicCount; j++)
                    {
                        var music = allMusics[random.Next(0, allMusics.Count())];
                        var reportMusic = new ReportMusic()
                        {
                            UserId = randomUser.Id,
                            MusicId = music.Id,
                            Description = "Some description from user",
                            Type = (ReportMusicType)values.GetValue(random.Next(values.Length)),
                        };

                        allMusics.Remove(music);
                        await dbContext.ReportMusics.AddAsync(reportMusic);
                    }
                }
            }
        }
    }
}
