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
        public static List<User> Users => UserTestsData.Users;

        public static List<License> Licenses => LicenseTestsData.Licenses();

        public static List<Category> Categories => CategoryTestsData.GetCategories;

        public static List<Music> Musics => MusicTestsData.GetMusics;

        public static string CreateUserId => Users[0].Id;

        public static List<MusicLicense> MusicLicenses()
        {
            var result = new List<MusicLicense>();
            var random = new Random();
            var musics = Musics;
            var licenses = Licenses;
            var count = 1;
            foreach (var license in licenses)
            {
                var countMusicLicenses = random.Next(1, musics.Count - 1);
                var musicToLicense = new List<Music>(musics);
                if (countMusicLicenses > musicToLicense.Count * 0.4)
                {
                    for (int i = 0; i < countMusicLicenses; i++)
                    {
                        var music = musicToLicense[random.Next(0, musicToLicense.Count())];
                        var musicLicense = new MusicLicense()
                        {
                            Id = "musicLicense" + count,
                            LicenseId = license.Id,
                            MusicId = music.Id,
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
