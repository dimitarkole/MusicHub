using MusicHub.Data.Models;
using MusicHub.Web.ViewModels.PlaylistModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicHub.Tests.TestData
{
    public class PlaylistMusicTestsData
    {
        public static PlaylistMusicCreateInputModel CreateModel => new PlaylistMusicCreateInputModel()
        {
            MusicId = Musics[0].Id,
            PlaylistId = Playlists[0].Id
        };

        public static string PlaylistId => Playlists[0].Id;

        public static string DeletePlaylistMusicId => PlaylistMusics()[0].Id;

        public static List<User> Users => UserTestsData.Users;

        public static List<Category> Categories => CategoryTestsData.GetCategories;

        public static List<Music> Musics => MusicTestsData.GetMusics;

        public static List<Playlist> Playlists => PlaylistTestsData.Playlists;

        public static string UpdateFistPositionPlaylistMusicId => PlaylistMusics()
            .GroupBy(pm => pm.PlaylistId)
            .ToDictionary(pm => pm.Key, pm => pm.ToList())
            .OrderBy(pm => pm.Value.Count())
            .FirstOrDefault()
            .Value
            .OrderBy(pm => pm.Position)
            .FirstOrDefault()
            .Id;

        public static string UpdateLastPositionPlaylistMusicId() => PlaylistMusics()
            .GroupBy(pm => pm.PlaylistId)
            .ToDictionary(pm => pm.Key, pm => pm.ToList())
            .OrderBy(pm => pm.Value.Count())
            .FirstOrDefault()
            .Value
            .OrderByDescending(pm => pm.Position)
            .FirstOrDefault()
            .Id;

        public static int UpdateLastPositionNewPosition()
        {
            var lastPosition = PlaylistMusics()
                .GroupBy(pm => pm.PlaylistId)
                .ToDictionary(pm => pm.Key, pm => pm.ToList())
                .OrderBy(pm => pm.Value.Count())
                .FirstOrDefault()
                .Value
                .OrderByDescending(pm => pm.Position)
                .FirstOrDefault()
                .Position;

            return lastPosition - 3 >= 0 ? lastPosition - 3 : 0;
        }

        public static int UpdateFirstPositionNewPosition()
        {
            var playlistMusics = PlaylistMusics()
                .GroupBy(pm => pm.PlaylistId)
                .ToDictionary(pm => pm.Key, pm => pm.ToList())
                .OrderBy(pm => pm.Value.Count())
                .FirstOrDefault()
                .Value;
            var lastPosition = playlistMusics
                .OrderBy(pm => pm.Position)
                .FirstOrDefault()
                .Position;

            return lastPosition + 3 < playlistMusics.Count ? lastPosition + 3 : playlistMusics.Count;
        }

        public static List<PlaylistMusic> PlaylistMusics()
        {
            var result = new List<PlaylistMusic>();
            var random = new Random();
            var musics = Musics;
            var playlists = Playlists;
            var count = 1;
            foreach (var playlist in playlists)
            {
                var countMusicsInPlaylist = random.Next(1, musics.Count - 1);
                var musicToPlaylist = new List<Music>(musics);
                if(countMusicsInPlaylist > musicToPlaylist.Count * 0.4)
                {
                    for (int i = 0; i < countMusicsInPlaylist; i++)
                    {
                        var music = musicToPlaylist[random.Next(0, musicToPlaylist.Count())];
                        var playlistMusic = new PlaylistMusic()
                        {
                            Id = "playlistMusic" + count,
                            PlaylistId = playlist.Id,
                            MusicId = music.Id,
                            Position = i,
                        };
                        musicToPlaylist.Remove(music);

                        result.Add(playlistMusic);
                        count++;
                    }
                }
            }

            return result;
        }

      /*  public static List<PlaylistMusic> PlaylistMusics()
        {
            var result = new List<PlaylistMusic>();
            var random = new Random();
            var playlistMusicCount = random.Next(Playlists.Count * 3, Playlists.Count * 5);
            for (int i = 0; i < 10; i++)
            {
                var playlistId = Playlists[random.Next(1, Playlists.Count)].Id;
                var musicId = Musics[random.Next(0, Musics.Count)].Id;
                var position = result
                    .Where(pm => pm.PlaylistId == playlistId)
                    .Max(pm => pm.Position);
                var newPlaylistMusic = new PlaylistMusic()
                {
                    Id = "PlaylistMusic" + i + 1,
                    MusicId = musicId,
                    PlaylistId = playlistId,
                    Position = position,
                };
            }

            for (int i = 0; i < playlistMusicCount; i++)
            {
                var playlistId = Playlists[random.Next(1, Playlists.Count)].Id;
                var musicId = Musics[random.Next(0, Musics.Count)].Id;
                var position = result
                    .Where(pm => pm.PlaylistId == playlistId)
                    .Max(pm => pm.Position);
                var newPlaylistMusic = new PlaylistMusic()
                {
                    Id = "PlaylistMusic" + i+1,
                    MusicId = musicId,
                    PlaylistId = playlistId,
                    Position = position,
                };

                result.Add(newPlaylistMusic);
            }

            return result;
        }*/
    }
}
