using MusicHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHub.Data.Seeding
{
    public class PlaylistSongSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var random = new Random();
            var songs = dbContext.Songs
              .ToArray();
            var playlists = dbContext.Playlists
              .ToArray();

            foreach (var playlist in playlists)
            {
                var countSongInPlaylist = random.Next(1, songs.Length - 1);
                var songToPlaylist = new List<Song>(songs);
                for (int i = 0; i < countSongInPlaylist; i++)
                {
                    var song = songToPlaylist[random.Next(0, songToPlaylist.Count())];
                    var playlistSong = new PlaylistSong()
                    {
                        PlaylistId = playlist.Id,
                        SongId = song.Id,
                    };
                    songToPlaylist.Remove(song);

                    await dbContext.PlaylistSongs.AddAsync(playlistSong);
                }
            }
        }
    }
}
