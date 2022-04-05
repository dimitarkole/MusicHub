namespace MusicHub.Tests.TestData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using MusicHub.Data.Models;
    using MusicHub.Web.ViewModels.PlayLaterMusicModels;

    public class PlayLaterMusicTestsData
    {
        public static PlayLaterMusicCreateInputModel CreateModel => new PlayLaterMusicCreateInputModel()
        {
            MusicId = MusicTestsData.GetMusics[0].Id,
        };

        public static PlayLaterMusicCreateInputModel CreateEmptyModel => new PlayLaterMusicCreateInputModel()
        {
            MusicId = "",
        };

        public static PlayLaterMusicEditInputModel EditModel => new PlayLaterMusicEditInputModel()
        {
            Position = 1,
        };

        public static PlayLaterMusicEditInputModel EditEmptyModel => new PlayLaterMusicEditInputModel()
        {
            Position = -2,
        };

        public static List<User> Users => UserTestsData.Users;

        public static PlayLaterMusicFilterInputModels FilterModel => new PlayLaterMusicFilterInputModels()
        {
            Name = "1",
        };

        public static List<Category> Categories => CategoryTestsData.GetCategories;

        public static List<Music> Musics => MusicTestsData.GetMusics;

        public static List<PlayLaterMusic> PlayLaterMusics()
        {
            var result = new List<PlayLaterMusic>();
            var random = new Random();
            var musics = Musics;
            var users = Users;
            var count = 1;
            foreach (var user in users)
            {
                var countPlayedMusics = random.Next(1, musics.Count - 1);
                if (countPlayedMusics > musics.Count * 0.4)
                {
                    var playLaterMusics = new List<Music>(musics);
                    for (int i = 0; i < countPlayedMusics; i++)
                    {
                        var music = playLaterMusics[random.Next(0, playLaterMusics.Count())];
                        var playLaterMusic = new PlayLaterMusic()
                        {
                            Id = "playedMusic" + count,
                            UserId = user.Id,
                            MusicId = music.Id,
                        };

                        playLaterMusics.Remove(music);
                        result.Add(playLaterMusic);
                        count++;
                    }
                }
            }

            return result;
        }

        public static int UpdateFirstPositionNewPosition(List<PlayLaterMusic> playLaterMusics, string playLaterMusicId)
        {
            var userId = playLaterMusics
                .Where(plm => plm.Id == playLaterMusicId)
                .FirstOrDefault()
                .UserId;
            var userPlayLaterMusics = playLaterMusics
                .Where(plm => plm.UserId == userId)
                .ToList();
            var lastPosition = playLaterMusics
                .Where(plm => plm.Id == playLaterMusicId)
                .FirstOrDefault()
                .Position;
            return lastPosition + 3 < userPlayLaterMusics.Count ? lastPosition + 3 : userPlayLaterMusics.Count;
        }
    }
}
