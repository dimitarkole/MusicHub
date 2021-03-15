using MusicHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Tests.TestData
{
    public class PlaylistMusicTestsData
    {
        public static List<User> Users => UserTestsData.Users;

        public static List<Music> Musics => MusicTestsData.GetMusics;

        public static List<Playlist> Playlists => PlaylistTestsData.Playlists;

        public static List<PlaylistMusic> PlaylistMusics()
        {
            var result = new List<PlaylistMusic>();
            var random = new Random();
            var playlistMusicCount = random.Next(Playlists.Count * 3, Playlists.Count * 5);
            for (int i = 0; i < playlistMusicCount; i++)
            {
                var playlistId = Playlists[random.Next(0, Playlists.Count)].Id;
                var musicId = Musics[random.Next(0, Musics.Count)].Id;
                var newPlaylistMusic = new PlaylistMusic()
                {
                    Id = "PlaylistMusic" + i+1,
                    MusicId = musicId,
                    PlaylistId = playlistId,
                };

                result.Add(newPlaylistMusic);
            }

            return result;
        }
    }
}
