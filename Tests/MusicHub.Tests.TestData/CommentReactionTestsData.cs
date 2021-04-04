namespace MusicHub.Tests.TestData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.Web.ViewModels.CommentModels;
    using MusicHub.Web.ViewModels.MusicReactionModels;

    public static class CommentReactionTestsData
    {
        public static CommentReactionCreateModel CreateModel => new CommentReactionCreateModel()
        {
            CommentId = Comments[0].Id,
            Reaction = Reaction.Dislike,
        };

        public static CommentReactionEditInputModel UpdateModel => new CommentReactionEditInputModel()
        {
            Reaction = Reaction.Like,
        };

        public static string CreateUserId => UserTestsData.Users[0].Id;

        public static List<Category> Categories => CategoryTestsData.GetCategories;

        public static List<User> Users => UserTestsData.Users;

        public static List<Music> Musics => MusicTestsData.GetMusics;

        public static List<Comment> Comments => CommentTestsData.GetComments;

        public static List<CommentReaction> CommentReactions()
        {
            var result = new List<CommentReaction>();
            var random = new Random();
            var allComments = Comments;
            var users = Users;
            var count = 1;
            foreach (var user in users)
            {
                var countPlayedMusics = random.Next(1, allComments.Count - 1);
                if (countPlayedMusics > allComments.Count * 0.4)
                {
                    var comments = new List<Comment>(allComments);
                    for (int i = 0; i < countPlayedMusics; i++)
                    {
                        var comment = comments[random.Next(0, comments.Count())];
                        var reaction = Reaction.None;
                        while (reaction == Reaction.None)
                        {
                            Type type = typeof(Reaction);

                            Array values = type.GetEnumValues();
                            reaction = (Reaction)values.GetValue(random.Next(values.Length));
                        }

                        var commentReaction = new CommentReaction()
                        {
                            Id = "reactionMusic" + count,
                            UserId = user.Id,
                            Reaction = reaction,
                            CommentId = comment.Id,
                        };

                        comments.Remove(comment);
                        result.Add(commentReaction);
                        count++;
                    }
                }
            }

            return result;
        }
    }
}
