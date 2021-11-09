using MusicHub.Common;
using MusicHub.Data.Models;
using MusicHub.Data.Models.Enums;
using MusicHub.Web.ViewModels.MusicModels;
using MusicHub.Web.ViewModels.MusicReactionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicHub.Tests.TestData
{
    public static class MusicReactionTestsData
    {
        public static MusicReactionCreateInputModel CreateModel => new MusicReactionCreateInputModel()
        {
            MusicId = Musics[0].Id,
            Reaction =Reaction.Dislike,
        };

        public static MusicReactionCreateInputModel UpdateModel => new MusicReactionCreateInputModel()
        {
            Reaction = Reaction.Like,
        };

        public static string CreateUserId => UserTestsData.Users[0].Id;

        public static List<User> Users => UserTestsData.Users;

        public static List<Category> Categories => CategoryTestsData.GetCategories;

        public static List<Music> Musics => MusicTestsData.GetMusics;

        public static MusicFilter FilterWithCategory => new MusicFilter()
        {
            CategoryId = Categories[0].Id,
            Name = "1",
            OrderMethod = OrderMethod.CreatedOnAsc,
        };

        public static MusicFilter Filter => new MusicFilter()
        {
            Name = "1",
            OrderMethod = OrderMethod.CreatedOnAsc,
        };

        public static List<MusicReaction> MusicReactions()
        {
            var result = new List<MusicReaction>();
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
                        var reaction = Reaction.None;
                        while(reaction == Reaction.None)
                        {
                            Type type = typeof(Reaction);

                            Array values = type.GetEnumValues();
                            reaction = (Reaction)values.GetValue(random.Next(values.Length));
                        }

                        var playedMusic = new MusicReaction()
                        {
                            Id = "reactionMusic" + count,
                            UserId = user.Id,
                            Reaction = reaction,
                            MusicId = music.Id,
                            ModifiedOn = null,
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
