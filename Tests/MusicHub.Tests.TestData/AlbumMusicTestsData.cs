namespace MusicHub.Tests.TestData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using MusicHub.Data.Models;
    using MusicHub.Web.ViewModels.AlbumMusicModels;

    public static class AlbumMusicTestsData
    {
        public static AlbumMusicCreateInputModel CreateModelWithOwnedMusic => new AlbumMusicCreateInputModel()
        {
            MusicId = Musics.Where(m => m.UserId == UserId).FirstOrDefault().Id,
            AlbumId = Albums[0].Id,
        };

        public static AlbumMusicCreateInputModel CreateModelWithNotOwnedMusic => new AlbumMusicCreateInputModel()
        {
            MusicId = Musics.Where(m => !(m.UserId == UserId)).FirstOrDefault().Id,
            AlbumId = Albums[0].Id,
        };

        public static string UserId => Users[0].Id;

        public static string AlbumId => Albums[0].Id;

        public static string DeleteAlbumMusicId => AlbumMusics()[0].Id;

        public static List<User> Users => UserTestsData.Users;

        public static List<Category> Categories => CategoryTestsData.GetCategories;

        public static List<Music> Musics => MusicTestsData.GetMusics;

        public static List<Album> Albums => AlbumTestsData.Albums;

        public static string UpdateFistPositionAlbumMusicId => AlbumMusics()
            .GroupBy(am => am.AlbumId)
            .ToDictionary(am => am.Key, am => am.ToList())
            .OrderBy(pm => pm.Value.Count())
            .FirstOrDefault()
            .Value
            .OrderBy(pm => pm.Position)
            .FirstOrDefault()
            .Id;

        public static string UpdateLastPositionAlbumMusicId() => AlbumMusics()
            .GroupBy(am => am.AlbumId)
            .ToDictionary(am => am.Key, am => am.ToList())
            .OrderBy(am => am.Value.Count())
            .FirstOrDefault()
            .Value
            .OrderByDescending(pm => pm.Position)
            .FirstOrDefault()
            .Id;

        public static int UpdateLastPositionNewPosition()
        {
            var lastPosition = AlbumMusics()
                .GroupBy(am => am.AlbumId)
                .ToDictionary(am => am.Key, am => am.ToList())
                .OrderBy(am => am.Value.Count())
                .FirstOrDefault()
                .Value
                .OrderByDescending(am => am.Position)
                .FirstOrDefault()
                .Position;

            return lastPosition - 3 >= 0 ? lastPosition - 3 : 0;
        }

        public static int UpdateFirstPositionNewPosition()
        {
            var albumMusics = AlbumMusics()
                .GroupBy(am => am.AlbumId)
                .ToDictionary(am => am.Key, am => am.ToList())
                .OrderBy(am => am.Value.Count())
                .FirstOrDefault()
                .Value;
            var lastPosition = albumMusics
                .OrderBy(am => am.Position)
                .FirstOrDefault()
                .Position;

            return lastPosition + 3 < albumMusics.Count ? lastPosition + 3 : albumMusics.Count;
        }

        public static List<AlbumMusic> AlbumMusics()
        {
            var result = new List<AlbumMusic>();
            var random = new Random();
            var musics = Musics;
            var albums = Albums;
            var count = 1;
            foreach (var album in albums)
            {
                var countMusicsInAlbum = random.Next(1, musics.Count - 1);
                var musicToAlbum = new List<Music>(musics);
                if (countMusicsInAlbum > musicToAlbum.Count * 0.4)
                {
                    for (int i = 0; i < countMusicsInAlbum; i++)
                    {
                        var music = musicToAlbum[random.Next(0, musicToAlbum.Count())];
                        var albumMusic = new AlbumMusic()
                        {
                            Id = "AlbumMusic" + count,
                            AlbumId = album.Id,
                            MusicId = music.Id,
                            Position = i,
                        };

                        musicToAlbum.Remove(music);
                        result.Add(albumMusic);
                        count++;
                    }
                }
            }

            return result;
        }
    }
}
