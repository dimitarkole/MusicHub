namespace MusicHub.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MusicHub.Data.Models;

    using static MusicHub.Common.GlobalConstants;

    public class MusicsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbConDescription, IServiceProvider serviceProvider)
        {
            var musics = this.GetMusics();
            var userRoleId = dbConDescription.Roles
                .Where(r => r.Name == Roles.User)
                .FirstOrDefault()
                .Id;
            var users = dbConDescription.Users
                  .Where(u => u.Roles.Any(r => r.RoleId == userRoleId))
                  .ToArray();
            var categories = dbConDescription.Categories
                .ToArray();

            foreach (var music in musics)
            {
                var random = new Random();
                var user = users[random.Next(0, users.Count())];
                var category = categories[random.Next(0, categories.Count())];
                if (!dbConDescription.Musics.Any(e => e.Name == music.Name))
                {
                    music.UserId = user.Id;
                    music.User = user;
                    music.Category = category;
                    music.CategoryId = category.Id;
                    await dbConDescription.Musics.AddAsync(music);
                }
            }
        }

        private Music[] GetMusics()
            => new Music[]
                {
                    new Music()
                    {
                        Name = "Ariana Grande - 7 rings.mp3",
                        Description = "Ariana Grande is the best",
                        AudioUrl = "Ariana Grande - 7 rings (Lyrics).mp3",
                        ImageUrl = "ariana_grande_7_rings_official_video.jpg",
                    },

                    new Music()
                    {
                        Name = "Dj tzepesh saxophone original track",
                        Description = "Dj tzepesh saxophone",
                        AudioUrl = "Dj TZepesh-Saxophone (Original Track).mp3",
                        ImageUrl = "MusicDefaultImage.jpg",
                    },

                    new Music()
                    {
                        Name = "Eminem - the goat",
                        Description = "Eminem music",
                        AudioUrl = "Eminem  Respect The GOAT 2019.mp3",
                        ImageUrl = "MusicDefaultImage.jpg",
                    },

                    new Music()
                    {
                        Name = "Егор Крид feat. Филипп Киркоров - Цвет настроения черный",
                        Description = "Rushan music",
                        AudioUrl = "Егор Крид Feat. Филипп Киркоров Цвет настроения черный.mp3",
                        ImageUrl = "MusicDefaultImage.jpg",
                    },

                    new Music()
                    {
                        Name = "Sevyn Streeter - how bad do you want it",
                        Description = "Sevyn Streeter is the best",
                        AudioUrl = "Sevyn Streeter - How Bad Do You Want It (Official Video).mp3",
                        ImageUrl = "MusicDefaultImage.jpg",
                    },

                    new Music()
                    {
                        Name = "Shakira - Loca",
                        Description = "Shakira music",
                        AudioUrl = "sShakira - Loca.mp3",
                        ImageUrl = "MusicDefaultImage.jpg",
                    },

                    new Music()
                    {
                        Name = "Yuri Boyka - can't be touched",
                        Description = "can't be touched",
                        AudioUrl = "Yuri Boyka - Can't Be Touched.mp3",
                        ImageUrl = "MusicDefaultImage.jpg",
                    },

                    new Music()
                    {
                        Name = "Джиган, Тимати, Егор Крид - Rolls Royce",
                        Description = "Rushan music",
                        AudioUrl = "Джиган, Тимати, Егор Крид - Rolls Royce.mp3",
                        ImageUrl = "MusicDefaultImage.jpg",
                    },

                    new Music()
                    {
                        Name = "СофтУни екип - Мотивирай се",
                        Description = "Мотивирай се, пенен на СофтУни",
                        AudioUrl = "СофтУни екип  Мотивирай се официално видео.mp3",
                        ImageUrl = "MusicDefaultImage.jpg",
                    },

                    new Music()
                    {
                        Name = "Филипп Киркоров - Любовь или обман",
                        Description = "Песен Филипп Киркоров - Любовь или обман.mp3",
                        AudioUrl = "Филипп Киркоров - Любовь или обман.mp3",
                        ImageUrl = "MusicDefaultImage.jpg",
                    },

                    new Music()
                    {
                        Name = "dj sava ft andreea - money maker",
                        Description = "money maker",
                        AudioUrl = "DJ Sava feat. Andreea D & J. Yolo - Money Maker (Video).mp3",
                        ImageUrl = "MusicDefaultImage.jpg",
                    },

                    new Music()
                    {
                        Name = "Gazirovka_black",
                        Description = "gazirovka black",
                        AudioUrl = "GAZIROVKA - Black (2017).mp3",
                        ImageUrl = "MusicDefaultImage.jpg",
                    },
                };
    }
}
