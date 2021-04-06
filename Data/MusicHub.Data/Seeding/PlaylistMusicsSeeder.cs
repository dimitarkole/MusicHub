namespace MusicHub.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Data.Models;

    public class PlaylistMusicsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.PlaylistMusics.Count() == 0)
            {
                var random = new Random();
                var musics = dbContext.Musics
                  .ToArray();
                var playlists = dbContext.Playlists
                  .ToArray();

                foreach (var playlist in playlists)
                {
                    var countSongInPlaylist = random.Next(1, musics.Length - 1);
                    var musicToPlaylist = new List<Music>(musics);
                    for (int i = 0; i < countSongInPlaylist; i++)
                    {
                        var music = musicToPlaylist[random.Next(0, musicToPlaylist.Count())];
                        var playedMusic = new PlaylistMusic()
                        {
                            PlaylistId = playlist.Id,
                            MusicId = music.Id,
                        };
                        musicToPlaylist.Remove(music);

                        await dbContext.PlaylistMusics.AddAsync(playedMusic);
                    }
                }
            }
        }
    }
}
