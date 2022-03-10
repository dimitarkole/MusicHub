using MusicHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHub.Data.Seeding
{
    public class PlayLaterMusicsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.PlayLaterMusics.Count() == 0)
            {
                var random = new Random();
                var musics = dbContext.Musics
                  .ToArray();
                var users = dbContext.Users.ToArray();
                foreach (var user in users)
                {
                    var countPlayLaterMusics = random.Next(1, musics.Length - 1);
                    var allMusics = new List<Music>(musics);
                    int position = 0;
                    for (int i = 0; i < countPlayLaterMusics; i++)
                    {
                        var music = allMusics[random.Next(0, allMusics.Count())];
                        var playLeterMusic = new PlayLaterMusic()
                        {
                            UserId = user.Id,
                            MusicId = music.Id,
                            Position = position,
                        };

                        position++;
                        allMusics.Remove(music);
                        await dbContext.PlayLaterMusics.AddAsync(playLeterMusic);
                    }
                }
            }
        }
    }
}
