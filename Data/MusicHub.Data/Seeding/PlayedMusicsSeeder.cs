using MusicHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHub.Data.Seeding
{
    public class PlayedMusicsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.PlayedMusics.Count() == 0)
            {
                var random = new Random();
                var musics = dbContext.Musics
                  .ToArray();
                var users = dbContext.Users.ToArray();

                foreach (var user in users)
                {
                    var countPlayedMusics = random.Next(1, musics.Length - 1);
                    var allMusics = new List<Music>(musics);
                    for (int i = 0; i < countPlayedMusics; i++)
                    {
                        var music = allMusics[random.Next(0, allMusics.Count())];
                        var playedMusic = new PlayedMusic()
                        {
                            UserId = user.Id,
                            MusicId = music.Id,
                        };

                        allMusics.Remove(music);
                        await dbContext.PlayedMusics.AddAsync(playedMusic);
                    }
                }
            }
        }
    }
}
