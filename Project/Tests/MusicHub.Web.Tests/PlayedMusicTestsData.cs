using MusicHub.Data.Models;
using MusicHub.Web.ViewModels.SongViewHistoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicHub.Tests.TestData
{
    public class PlayedMusicTestsData
    {
        public static SongViewHistoryCreateInputModel CreateModel => new SongViewHistoryCreateInputModel()
        {
            SongId = SongTestData.GetSongs[0].Id
        };

        public static List<ApplicationUser> Users => UserTestsData.Users;

        public static SongViewHistoryFilterInputModels filterModel => new SongViewHistoryFilterInputModels()
        {
            Name = "1",
        };

        public static List<Category> Categories => CategoryTestsData.GetCategories;

        public static List<Song> Musics => SongTestData.GetSongs;

        public static List<SongViewHistory> PlayedMusics()
        {
            var result = new List<SongViewHistory>();
            var random = new Random();
            var musics = Musics;
            var users = Users;
            var count = 1;
            foreach (var user in users)
            {
                var countPlayedMusics = random.Next(1, musics.Count - 1);
                if (countPlayedMusics > musics.Count * 0.4)
                {
                    var playedMusics = new List<Song>(musics);
                    for (int i = 0; i < countPlayedMusics; i++)
                    {
                        var music = playedMusics[random.Next(0, playedMusics.Count())];
                        var playedMusic = new SongViewHistory()
                        {
                            Id = "playedMusic" + count,
                            UserId = user.Id,
                            SongId = music.Id,
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
