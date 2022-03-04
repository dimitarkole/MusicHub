namespace MusicHub.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Data.Models;

    public class AlbumMusicsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.AlbumMusics.Count() == 0)
            {
                var random = new Random();
                var musics = dbContext.Musics.ToArray();
                var albums = dbContext.Albums.ToArray();
                foreach (var album in albums)
                {
                    var countMusicInAlbum = random.Next(1, musics.Length - 1);
                    var musicToAlbum = new List<Music>(musics);
                    for (int i = 0; i < countMusicInAlbum; i++)
                    {
                        var music = musicToAlbum[random.Next(0, musicToAlbum.Count())];
                        var albumMusic = new AlbumMusic()
                        {
                            AlbumId = album.Id,
                            MusicId = music.Id,
                        };

                        musicToAlbum.Remove(music);
                        await dbContext.AlbumMusics.AddAsync(albumMusic);
                    }
                }
            }
        }
    }
}
