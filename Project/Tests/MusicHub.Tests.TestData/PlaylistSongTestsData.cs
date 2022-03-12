using MusicHub.Data.Models;
using MusicHub.Web.ViewModels.PlaylistModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicHub.Tests.TestData
{
    public class PlaylistSongTestsData
    {
        public static SongToPlaylistCreateInputModel CreateModel => new SongToPlaylistCreateInputModel()
        {
            SongId = Musics[0].Id,
            PlaylistId = Playlists[0].Id
        };

        public static string PlaylistId => Playlists[0].Id;

        public static List<Playlist> Playlists => PlaylistTestsData.Playlists;

        public static string DeletePlaylistSongId => PlaylistSongs()[0].Id;

        public static List<ApplicationUser> Users => UserTestsData.Users;

        public static List<Category> Categories => CategoryTestsData.GetCategories;

        public static List<Song> Musics => SongTestData.GetSongs;


        public static List<PlaylistSong> PlaylistSongs()
        {
            var result = new List<PlaylistSong>();
            var random = new Random();
            var musics = Musics;
            var playlists = Playlists;
            var count = 1;
            foreach (var playlist in playlists)
            {
                var countMusicsInPlaylist = random.Next(1, musics.Count - 1);
                var musicToPlaylist = new List<Song>(musics);
                if(countMusicsInPlaylist > musicToPlaylist.Count * 0.4)
                {
                    for (int i = 0; i < countMusicsInPlaylist; i++)
                    {
                        var music = musicToPlaylist[random.Next(0, musicToPlaylist.Count())];
                        var playlistMusic = new PlaylistSong()
                        {
                            Id = "playlistMusic" + count,
                            PlaylistId = playlist.Id,
                            SongId = music.Id,
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
