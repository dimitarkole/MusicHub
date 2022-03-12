namespace MusicHub.Tests.TestData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using MusicHub.Data.Models;
    using MusicHub.Web.ViewModels.CommentModels;

    public static class CommentTestsData
    {
        public static CommentCreatreInputModel CreateModel => new CommentCreatreInputModel()
        {
            SongId = Musics[0].Id,
            Text = "simple comment"
        };

        public static string UpdateCommentId => GetComments[0].Id;

        public static CommentEditInputModel UpdateModel => new CommentEditInputModel()
        {
            Text = "simple comment",
        };

        public static CommentChildrenCreatreInputModel CreateChildModel => new CommentChildrenCreatreInputModel()
        {
            Text = "simple comment",
        };

        public static string CreateUserId => Users[0].Id;

        public static string CreateParentCommentId => GetComments[0].Id;

        public static List<Category> Categories => CategoryTestsData.GetCategories;

        public static List<ApplicationUser> Users => UserTestsData.Users;

        public static List<Song> Musics => SongTestData.GetSongs;

        public static string DeleteCommentId => GetComments[0].Id;

        public static string MusicId => GetComments[0].SongId;

        public static string IsOwnedCommentTrueCommentId => GetComments[0].Id;

        public static string IsOwedCommentTrueUserId => GetComments[0].UserId;

        public static string IsOwnedCommentFalseCommentId => GetComments[0].Id;

        public static string IsOwnedCommentFalseUserId => Users
            .Where(u => u.Id != GetComments.Where(c => c.Id != IsOwnedCommentFalseCommentId)
                .FirstOrDefault()
                .UserId)
            .FirstOrDefault()
            .Id;

        public static List<Comment> GetComments => new List<Comment>()
        {
            new Comment() {Id ="CommentId1", Text = "simple text1", UserId = Users[0].Id, SongId = Musics[2].Id, CommentReactions = new List<CommentReaction>(), CommentsChildren = new List<Comment>(), ParentCommentId = null },
            new Comment() {Id ="CommentId2", Text = "simple text1", UserId = Users[1].Id, SongId = Musics[3].Id, CommentReactions = new List<CommentReaction>(), CommentsChildren = new List<Comment>(), ParentCommentId = null},
            new Comment() {Id ="CommentId3", Text = "simple text1", UserId = Users[2].Id, SongId = Musics[1].Id, CommentReactions = new List<CommentReaction>(), CommentsChildren = new List<Comment>(), ParentCommentId = null},
            new Comment() {Id ="CommentId4", Text = "simple text1", UserId = Users[2].Id, SongId = Musics[5].Id, CommentReactions = new List<CommentReaction>(), CommentsChildren = new List<Comment>(), ParentCommentId = null},
            new Comment() {Id ="CommentId5", Text = "simple text1", UserId = Users[2].Id, SongId = Musics[6].Id, CommentReactions = new List<CommentReaction>(), CommentsChildren = new List<Comment>(), ParentCommentId = null},
            new Comment() {Id ="CommentId6", Text = "simple text1", UserId = Users[1].Id, SongId = Musics[4].Id, CommentReactions = new List<CommentReaction>(), CommentsChildren = new List<Comment>(), ParentCommentId = null},
            new Comment() {Id ="CommentId7", Text = "simple text1", UserId = Users[2].Id, SongId = Musics[2].Id, CommentReactions = new List<CommentReaction>(), CommentsChildren = new List<Comment>(), ParentCommentId = null},
            new Comment() {Id ="CommentId8", Text = "simple text1", UserId = Users[3].Id, SongId = Musics[2].Id, CommentReactions = new List<CommentReaction>(), CommentsChildren = new List<Comment>(), ParentCommentId = null},
            new Comment() {Id ="CommentId9", Text = "simple text1", UserId = Users[4].Id, SongId = Musics[3].Id, CommentReactions = new List<CommentReaction>(), CommentsChildren = new List<Comment>(), ParentCommentId = null},
            new Comment() {Id ="CommentId10", Text = "simple text1", UserId = Users[2].Id, SongId = Musics[1].Id, CommentReactions = new List<CommentReaction>(), CommentsChildren = new List<Comment>(), ParentCommentId = null},
            new Comment() {Id ="CommentId11", Text = "simple text1", UserId = Users[1].Id, SongId = Musics[2].Id, CommentReactions = new List<CommentReaction>(), CommentsChildren = new List<Comment>(), ParentCommentId = null},
            new Comment() {Id ="CommentId12", Text = "simple text1", UserId = Users[3].Id, SongId = Musics[3].Id, CommentReactions = new List<CommentReaction>(), CommentsChildren = new List<Comment>(), ParentCommentId = null},
            new Comment() {Id ="CommentId13", Text = "simple text1", UserId = Users[1].Id, SongId = Musics[4].Id, CommentReactions = new List<CommentReaction>(), CommentsChildren = new List<Comment>(), ParentCommentId = null},
            new Comment() {Id ="CommentId14", Text = "simple text1", UserId = Users[2].Id, SongId = Musics[6].Id, CommentReactions = new List<CommentReaction>(), CommentsChildren = new List<Comment>(), ParentCommentId = null},
            new Comment() {Id ="CommentId15", Text = "simple text1", UserId = Users[2].Id, SongId = Musics[1].Id, CommentReactions = new List<CommentReaction>(), CommentsChildren = new List<Comment>(), ParentCommentId = null},
            new Comment() {Id ="CommentId16", Text = "simple text1", UserId = Users[3].Id, SongId = Musics[1].Id, CommentReactions = new List<CommentReaction>(), CommentsChildren = new List<Comment>(), ParentCommentId = null},
            new Comment() {Id ="CommentId17", Text = "simple text1", UserId = Users[0].Id, SongId = Musics[0].Id, CommentReactions = new List<CommentReaction>(), CommentsChildren = new List<Comment>(), ParentCommentId = null},
        };

    }
}
