namespace MusicHub.Tests.TestData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using MusicHub.Data.Models;
    using MusicHub.Web.ViewModels.LicenseMusicModels;

    public static class LicenseMusicTestsData
    {
        public static List<ApplicationUser> Users => UserTestsData.Users;

        public static List<License> Licenses => LicenseTestsData.Licenses();

        public static List<Category> Categories => CategoryTestsData.GetCategories;

        public static List<Song> Musics =>SongTestData.GetSongs;

        public static string CreateUserId => Users[0].Id;

        public static List<SongLicense> MusicLicenses()
        {
            var result = new List<SongLicense>();
            var random = new Random();
            var musics = Musics;
            var licenses = Licenses;
            var count = 1;
            foreach (var license in licenses)
            {
                var countMusicLicenses = random.Next(1, musics.Count - 1);
                var musicToLicense = new List<Song>(musics);
                if (countMusicLicenses > musicToLicense.Count * 0.4)
                {
                    for (int i = 0; i < countMusicLicenses; i++)
                    {
                        var music = musicToLicense[random.Next(0, musicToLicense.Count())];
                        var musicLicense = new SongLicense()
                        {
                            Id = "musicLicense" + count,
                            LicenseId = license.Id,
                            SongId = music.Id,
                        };
                        musicToLicense.Remove(music);

                        result.Add(musicLicense);
                        count++;
                    }
                }
            }

            return result;
        }

    }
}
