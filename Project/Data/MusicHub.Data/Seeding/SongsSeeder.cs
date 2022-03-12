namespace MusicHub.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Data.Models;
    using static MusicHub.Common.GlobalConstants;

    public class SongsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var songs = this.GetSongs();
            var userRoleId = dbContext.Roles
                .Where(r => r.Name == Roles.UserRoleName)
                .FirstOrDefault()
                .Id;
            var users = dbContext.Users
                  .Where(u => u.Roles.Any(r => r.RoleId == userRoleId))
                  .ToArray();
            var categories = dbContext.Categories
                .ToArray();
            foreach (var song in songs)
            {
                var random = new Random();
                var user = users[random.Next(0, users.Count())];
                var category = categories[random.Next(0, categories.Count())];
                if (!dbContext.Songs.Any(e => e.Name == song.Name))
                {
                    song.UserId = user.Id;
                    song.User = user;
                    song.Category = category;
                    song.CategoryId = category.Id;
                    await dbContext.Songs.AddAsync(song);
                }
            }
        }

        private Song[] GetSongs()
            => new Song[]
                {
                    new Song()
                    {
                        Name = "Ariana Grande - 7 rings.mp3",
                        Text = "Ariana Grande is the best",
                        AudioFilePath = "Ariana Grande - 7 rings (Lyrics).mp3",
                        ImageFilePath = "ariana_grande_7_rings_official_video.jpg",
                    },

                    new Song()
                    {
                        Name = "Dj tzepesh saxophone original track",
                        Text = "Dj tzepesh saxophone",
                        AudioFilePath = "Dj TZepesh-Saxophone (Original Track).mp3",
                        ImageFilePath = "songDefaultImage.jpg",
                    },

                    new Song()
                    {
                        Name = "Eminem - the goat",
                        Text = "Eminem music",
                        AudioFilePath = "Eminem  Respect The GOAT 2019.mp3",
                        ImageFilePath = "songDefaultImage.jpg",
                    },

                    new Song()
                    {
                        Name = "Егор Крид feat. Филипп Киркоров - Цвет настроения черный",
                        Text = "Rushan music",
                        AudioFilePath = "Егор Крид Feat. Филипп Киркоров Цвет настроения черный.mp3",
                        ImageFilePath = "songDefaultImage.jpg",
                    },

                    new Song()
                    {
                        Name = "Sevyn Streeter - how bad do you want it",
                        Text = "Sevyn Streeter is the best",
                        AudioFilePath = "Sevyn Streeter - How Bad Do You Want It (Official Video).mp3",
                        ImageFilePath = "songDefaultImage.jpg",
                    },

                    new Song()
                    {
                        Name = "Shakira - Loca",
                        Text = "Shakira music",
                        AudioFilePath = "sShakira - Loca.mp3",
                        ImageFilePath = "songDefaultImage.jpg",
                    },

                    new Song()
                    {
                        Name = "Yuri Boyka - can't be touched",
                        Text = "can't be touched",
                        AudioFilePath = "Yuri Boyka - Can't Be Touched.mp3",
                        ImageFilePath = "songDefaultImage.jpg",
                    },

                    new Song()
                    {
                        Name = "Джиган, Тимати, Егор Крид - Rolls Royce",
                        Text = "Rushan music",
                        AudioFilePath = "Джиган, Тимати, Егор Крид - Rolls Royce.mp3",
                        ImageFilePath = "songDefaultImage.jpg",
                    },

                    new Song()
                    {
                        Name = "СофтУни екип - Мотивирай се",
                        Text = "Мотивирай се, пенен на СофтУни",
                        AudioFilePath = "СофтУни екип  Мотивирай се официално видео.mp3",
                        ImageFilePath = "songDefaultImage.jpg",
                    },

                    new Song()
                    {
                        Name = "Филипп Киркоров - Любовь или обман",
                        Text = "Песен Филипп Киркоров - Любовь или обман.mp3",
                        AudioFilePath = "Филипп Киркоров - Любовь или обман.mp3",
                        ImageFilePath = "songDefaultImage.jpg",
                    },

                    new Song()
                    {
                        Name = "dj sava ft andreea - money maker",
                        Text = "money maker",
                        AudioFilePath = "DJ Sava feat. Andreea D & J. Yolo - Money Maker (Video).mp3",
                        ImageFilePath = "songDefaultImage.jpg",
                    },

                    new Song()
                    {
                        Name = "Gazirovka_black",
                        Text = "gazirovka black",
                        AudioFilePath = "GAZIROVKA - Black (2017).mp3",
                        ImageFilePath = "songDefaultImage.jpg",
                    },
                };
    }
}
