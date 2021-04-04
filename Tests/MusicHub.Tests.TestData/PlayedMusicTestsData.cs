using MusicHub.Data.Models;
using MusicHub.Web.ViewModels.PlayedMusicModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicHub.Tests.TestData
{
    public class PlayedMusicTestsData
    {
        public static PlayedMusicCreateInputModel CreateModel => new PlayedMusicCreateInputModel()
        {
            MusicId = MusicTestsData.GetMusics[0].Id
        };

        public static List<User> Users => UserTestsData.Users;

        public static PlayedMusicFilterInputModels filterModel => new PlayedMusicFilterInputModels()
        {
            Name = "1",
        };

        public static List<Category> Categories => CategoryTestsData.GetCategories;

        public static List<Music> Musics => MusicTestsData.GetMusics;

        public static List<PlayedMusic> PlayedMusics()
        {
            var result = new List<PlayedMusic>();
            var random = new Random();
            var musics = Musics;
            var users = Users;
            var count = 1;
            foreach (var user in users)
            {
                var countPlayedMusics = random.Next(1, musics.Count - 1);
                if (countPlayedMusics > musics.Count * 0.4)
                {
                    var playedMusics = new List<Music>(musics);
                    for (int i = 0; i < countPlayedMusics; i++)
                    {
                        var music = playedMusics[random.Next(0, playedMusics.Count())];
                        var playedMusic = new PlayedMusic()
                        {
                            Id = "playedMusic" + count,
                            UserId = user.Id,
                            MusicId = music.Id,
                        };

                        playedMusics.Remove(music);
                        result.Add(playedMusic);
                        count++;
                    }
                }
            }

            return result;
        }
    }
}
