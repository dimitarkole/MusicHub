namespace MusicHub.Tests.TestData
{
    using MusicHub.Common;
    using MusicHub.Data.Models;
    using MusicHub.Web.ViewModels.SongModels;
    using System.Collections.Generic;
    using System.Linq;
    public class SongTestData
    {
        public static SongCreateInputModel CreateModel => new SongCreateInputModel()
        {
            Name = "asd",
            CategoryId = Categories[0].Id,
            ImageFilePath = "image path.jpg",
            AudioFilePath = "audio path.mp3",
            Text = "this is test text",
        };

        public static string CreateUserId => Users[0].Id;

        public static string GetOwnSongsUserId => Users[0].Id;

        public static string IsOwnSongIdTrue => GetSongs[0].Id;

        public static string IsOwnSongUserIdTrue => Users
            .Where(u => u.Id == GetSongs[0].UserId)
            .FirstOrDefault()
            .Id;

        public static string IsOwnSongUserIdFalse => Users
            .Where(u => u.Id != GetSongs.Where(s => s.Id == IsOwnSongIdFalse).FirstOrDefault().UserId)
            .FirstOrDefault()
            .Id;

        public static string IsOwnSongIdFalse => GetSongs[0].Id;

        public static string DeleteSongId => GetSongs[0].Id;

        public static string GetById => GetSongs[0].Id;

        public static List<Category> Categories => new List<Category>()
        {
            new Category(){ Id ="testCategoryId1", Name = "testCategory1"},
            new Category(){ Id ="testCategoryId2", Name = "testCategory2"},
            new Category(){ Id ="testCategoryId3", Name = "testCategory3"},
            new Category(){ Id ="testCategoryId4", Name = "testCategory4"},
            new Category(){ Id ="testCategoryId5", Name = "testCategory5"},
        };

        public static List<Song> GetSongs => new List<Song>()
        {
            new Song(){ Id ="testMusicId1", Name = "testMusic1", CategoryId = Categories[0].Id, Text = "some Text", UserId = Users[0].Id, AudioFilePath="audio.mp3", ImageFilePath = "image.jpg", SongReactions = new List<SongReaction>(), PlaylistSongs = new List<PlaylistSong>(), SongViewHistories= new List<SongViewHistory>(), Comments = new List<Comment>(), MusicLicenseType = MusicLicenseType.CC, SongLicenses = new List<SongLicense>(), VisibleStatus = VisibleStatus.Public } ,
            new Song(){ Id ="testMusicId2", Name = "testMusic2", CategoryId = Categories[1].Id, Text = "some Text", UserId = Users[1].Id, AudioFilePath="audio.mp3", ImageFilePath = "image.jpg", Comments = new List<Comment>(), SongReactions = new List<SongReaction>(), PlaylistSongs = new List<PlaylistSong>(), SongViewHistories= new List<SongViewHistory>(), MusicLicenseType = MusicLicenseType.CC, SongLicenses = new List<SongLicense>(), VisibleStatus = VisibleStatus.Public },
            new Song(){ Id ="testMusicId3", Name = "testMusic3", CategoryId = Categories[0].Id, Text = "some Text", UserId = Users[1].Id, AudioFilePath="audio.mp3", ImageFilePath = "image.jpg", Comments = new List<Comment>(), SongReactions = new List<SongReaction>(), PlaylistSongs = new List<PlaylistSong>(), SongViewHistories= new List<SongViewHistory>(), MusicLicenseType = MusicLicenseType.CC, SongLicenses = new List<SongLicense>(), VisibleStatus = VisibleStatus.Hidden },
            new Song(){ Id ="testMusicId4", Name = "testMusic4", CategoryId = Categories[1].Id, Text = "some Text", UserId = Users[0].Id, AudioFilePath="audio.mp3", ImageFilePath = "image.jpg", Comments = new List<Comment>(), SongReactions = new List<SongReaction>(), PlaylistSongs = new List<PlaylistSong>(), SongViewHistories= new List<SongViewHistory>(), MusicLicenseType = MusicLicenseType.CC, SongLicenses = new List<SongLicense>(), VisibleStatus = VisibleStatus.Public },
            new Song(){ Id ="testMusicId5", Name = "testMusic5", CategoryId = Categories[1].Id, Text = "some Text", UserId = Users[2].Id, AudioFilePath="audio.mp3", ImageFilePath = "image.jpg", Comments = new List<Comment>(), SongReactions = new List<SongReaction>(), PlaylistSongs = new List<PlaylistSong>(), SongViewHistories= new List<SongViewHistory>(), MusicLicenseType = MusicLicenseType.CC, SongLicenses = new List<SongLicense>(), VisibleStatus = VisibleStatus.OnlyMe },
            new Song(){ Id ="testMusicId6", Name = "testMusic6", CategoryId = Categories[1].Id, Text = "some Text", UserId = Users[3].Id, AudioFilePath="audio.mp3", ImageFilePath = "image.jpg", Comments = new List<Comment>(), SongReactions = new List<SongReaction>(), PlaylistSongs = new List<PlaylistSong>(), SongViewHistories= new List<SongViewHistory>(), MusicLicenseType = MusicLicenseType.CC, SongLicenses = new List<SongLicense>(), VisibleStatus = VisibleStatus.Public },
            new Song(){ Id ="testMusicId7", Name = "testMusic7", CategoryId = Categories[1].Id, Text = "some Text", UserId = Users[0].Id, AudioFilePath="audio.mp3", ImageFilePath = "image.jpg", Comments = new List<Comment>(), SongReactions = new List<SongReaction>(), PlaylistSongs = new List<PlaylistSong>(), SongViewHistories= new List<SongViewHistory>(), MusicLicenseType = MusicLicenseType.CC, SongLicenses = new List<SongLicense>(), VisibleStatus = VisibleStatus.Public },
            new Song(){ Id ="testMusicId8", Name = "testMusic8", CategoryId = Categories[1].Id, Text = "some Text", UserId = Users[0].Id, AudioFilePath="audio.mp3", ImageFilePath = "image.jpg", Comments = new List<Comment>(), SongReactions = new List<SongReaction>(), PlaylistSongs = new List<PlaylistSong>(), SongViewHistories= new List<SongViewHistory>(), MusicLicenseType = MusicLicenseType.CC, SongLicenses = new List<SongLicense>(), VisibleStatus = VisibleStatus.OnlyMe },
            new Song(){ Id ="testMusicId9", Name = "testMusic9", CategoryId = Categories[1].Id, Text = "some Text", UserId = Users[1].Id, AudioFilePath="audio.mp3", ImageFilePath = "image.jpg", Comments = new List<Comment>(), SongReactions = new List<SongReaction>(), PlaylistSongs = new List<PlaylistSong>(), SongViewHistories= new List<SongViewHistory>(), MusicLicenseType = MusicLicenseType.CC, SongLicenses = new List<SongLicense>(), VisibleStatus = VisibleStatus.Public },
            new Song(){ Id ="testMusicId10", Name = "testMusic10", CategoryId = Categories[1].Id, Text = "some Text", UserId = Users[0].Id, AudioFilePath="audio.mp3", ImageFilePath = "image.jpg", Comments = new List<Comment>(), SongReactions = new List<SongReaction>(), PlaylistSongs = new List<PlaylistSong>(), SongViewHistories= new List<SongViewHistory>(), MusicLicenseType = MusicLicenseType.CC, SongLicenses = new List<SongLicense>(), VisibleStatus = VisibleStatus.Hidden },
            new Song(){ Id ="testMusicId11", Name = "testMusic11", CategoryId = Categories[1].Id, Text = "some Text", UserId = Users[2].Id, AudioFilePath="audio.mp3", ImageFilePath = "image.jpg", Comments = new List<Comment>(), SongReactions = new List<SongReaction>(), PlaylistSongs = new List<PlaylistSong>(), SongViewHistories= new List<SongViewHistory>(), MusicLicenseType = MusicLicenseType.CC, SongLicenses = new List<SongLicense>(), VisibleStatus = VisibleStatus.Public },
            new Song(){ Id ="testMusicId12", Name = "testMusic12", CategoryId = Categories[1].Id, Text = "some Text", UserId = Users[0].Id, AudioFilePath="audio.mp3", ImageFilePath = "image.jpg", Comments = new List<Comment>(), SongReactions = new List<SongReaction>(), PlaylistSongs = new List<PlaylistSong>(), SongViewHistories= new List<SongViewHistory>(), MusicLicenseType = MusicLicenseType.CC, SongLicenses = new List<SongLicense>(), VisibleStatus = VisibleStatus.Public },
            new Song(){ Id ="testMusicId13", Name = "testMusic13", CategoryId = Categories[1].Id, Text = "some Text", UserId = Users[1].Id, AudioFilePath="audio.mp3", ImageFilePath = "image.jpg", Comments = new List<Comment>(), SongReactions = new List<SongReaction>(), PlaylistSongs = new List<PlaylistSong>(), SongViewHistories= new List<SongViewHistory>(), MusicLicenseType = MusicLicenseType.CC, SongLicenses = new List<SongLicense>(), VisibleStatus = VisibleStatus.Public },
            new Song(){ Id ="testMusicId14", Name = "testMusic14", CategoryId = Categories[2].Id, Text = "some Text", UserId = Users[2].Id, AudioFilePath="audio.mp3", ImageFilePath = "image.jpg", Comments = new List<Comment>(), SongReactions = new List<SongReaction>(), PlaylistSongs = new List<PlaylistSong>(), SongViewHistories= new List<SongViewHistory>(),  MusicLicenseType = MusicLicenseType.CC, SongLicenses = new List<SongLicense>(), VisibleStatus = VisibleStatus.Public },
            new Song(){ Id ="testMusicId15", Name = "testMusic15", CategoryId = Categories[2].Id, Text = "some Text", UserId = Users[2].Id, AudioFilePath="audio.mp3", ImageFilePath = "image.jpg", Comments = new List<Comment>(), SongReactions = new List<SongReaction>(), PlaylistSongs = new List<PlaylistSong>(), SongViewHistories= new List<SongViewHistory>(),  MusicLicenseType = MusicLicenseType.CC, SongLicenses = new List<SongLicense>(), VisibleStatus = VisibleStatus.Public },
            new Song(){ Id ="testMusicId16", Name = "testMusic16", CategoryId = Categories[2].Id, Text = "some Text", UserId = Users[2].Id, AudioFilePath="audio.mp3", ImageFilePath = "image.jpg", Comments = new List<Comment>(), SongReactions = new List<SongReaction>(), PlaylistSongs = new List<PlaylistSong>(), SongViewHistories= new List<SongViewHistory>(),  MusicLicenseType = MusicLicenseType.CC, SongLicenses = new List<SongLicense>(), VisibleStatus = VisibleStatus.Public },
            new Song(){ Id ="testMusicId17", Name = "testMusic17", CategoryId = Categories[2].Id, Text = "some Text", UserId = Users[2].Id, AudioFilePath="audio.mp3", ImageFilePath = "image.jpg", Comments = new List<Comment>(), SongReactions = new List<SongReaction>(), PlaylistSongs = new List<PlaylistSong>(), SongViewHistories= new List<SongViewHistory>(),  MusicLicenseType = MusicLicenseType.CC, SongLicenses = new List<SongLicense>(), VisibleStatus = VisibleStatus.Public },
            new Song(){ Id ="testMusicId18", Name = "testMusic18", CategoryId = Categories[2].Id, Text = "some Text", UserId = Users[2].Id, AudioFilePath="audio.mp3", ImageFilePath = "image.jpg", Comments = new List<Comment>(), SongReactions = new List<SongReaction>(), PlaylistSongs = new List<PlaylistSong>(), SongViewHistories= new List<SongViewHistory>(),  MusicLicenseType = MusicLicenseType.CC, SongLicenses = new List<SongLicense>(), VisibleStatus = VisibleStatus.Public },

            /*new Song(){ Id ="testSongId1", Name = "testSong1", Category =Categories[0], CategoryId = Categories[0].Id, Text = "some text",Comments = new List<Comment>(), MusicLicenseType = Common.MusicLicenseType.CC, SongLicenses = new List<SongLicense>(),
                UserId = Users[0].Id, AudioFilePath="audio.mp3", ImageFilePath = "image.jpg", SongReactions = new List<SongReaction>(), PlaylistSongs = new List<PlaylistSong>(), SongViewHistories= new List<SongViewHistory>(),},
            new Song(){ Id ="testSongId2", Name = "testSong2", Category =Categories[1], CategoryId = Categories[1].Id, Text = "some text", UserId = Users[1].Id, AudioFilePath="audio.mp3", ImageFilePath = "image.jpg", Comments = new List<Comment>(), SongReactions = new List<SongReaction>(), PlaylistSongs = new List<PlaylistSong>(), SongViewHistories= new List<SongViewHistory>()},
            new Song(){ Id ="testSongId5", Name = "testSong5", Category =Categories[2], CategoryId = Categories[2].Id, Text = "some text", UserId = Users[2].Id, AudioFilePath="audio.mp3", ImageFilePath = "image.jpg", Comments = new List<Comment>(), SongReactions = new List<SongReaction>(), PlaylistSongs = new List<PlaylistSong>(), SongViewHistories= new List<SongViewHistory>()},
            new Song(){ Id ="testSongId3", Name = "testSong3", CategoryId = Categories[0].Id, Text = "some text", UserId = Users[1].Id, AudioFilePath="audio.mp3", ImageFilePath = "image.jpg", Comments = new List<Comment>(), SongReactions = new List<SongReaction>(), PlaylistSongs = new List<PlaylistSong>(), SongViewHistories= new List<SongViewHistory>()},
            new Song(){ Id ="testSongId4", Name = "testSong4", CategoryId = Categories[1].Id, Text = "some text", UserId = Users[0].Id, AudioFilePath="audio.mp3", ImageFilePath = "image.jpg", Comments = new List<Comment>(), SongReactions = new List<SongReaction>(), PlaylistSongs = new List<PlaylistSong>(), SongViewHistories= new List<SongViewHistory>()},
            new Song(){ Id ="testSongId5", Name = "testSong5", CategoryId = Categories[1].Id, Text = "some text", UserId = Users[0].Id, AudioFilePath="audio.mp3", ImageFilePath = "image.jpg", Comments = new List<Comment>(), SongReactions = new List<SongReaction>(), PlaylistSongs = new List<PlaylistSong>(), SongViewHistories= new List<SongViewHistory>()},
            new Song(){ Id ="testSongId6", Name = "testSong6", CategoryId = Categories[1].Id, Text = "some text", UserId = Users[0].Id, AudioFilePath="audio.mp3", ImageFilePath = "image.jpg", Comments = new List<Comment>(), SongReactions = new List<SongReaction>(), PlaylistSongs = new List<PlaylistSong>(), SongViewHistories= new List<SongViewHistory>()},
            new Song(){ Id ="testSongId7", Name = "testSong7", CategoryId = Categories[1].Id, Text = "some text", UserId = Users[0].Id, AudioFilePath="audio.mp3", ImageFilePath = "image.jpg", Comments = new List<Comment>(), SongReactions = new List<SongReaction>(), PlaylistSongs = new List<PlaylistSong>(), SongViewHistories= new List<SongViewHistory>()},
        */};

        public static List<ApplicationUser> Users => UserTestsData.Users;

        public static List<Comment> Comments => new List<Comment>()
        {
            new Comment(){Id ="testCommentId1", SongId = GetById, Text="comment1", UserId = Users[1].Id, CommentsChildren = new List<Comment>(), ParentCommentId = null, CommentReactions = new List<CommentReaction>()},
            new Comment(){Id ="testCommentId2", SongId = GetById, Text="comment2", UserId = Users[1].Id, CommentsChildren = new List<Comment>(), ParentCommentId = null, CommentReactions = new List<CommentReaction>()},
            new Comment(){Id ="testCommentId3", SongId = DeleteSongId, Text="comment3", UserId = Users[1].Id, CommentsChildren = new List<Comment>(), ParentCommentId = null, CommentReactions = new List<CommentReaction>()},
            new Comment(){Id ="testCommentId4", SongId = GetById, Text="comment4", UserId = Users[1].Id, CommentsChildren = new List<Comment>(), ParentCommentId = null, CommentReactions = new List<CommentReaction>()},
            new Comment(){Id ="testCommentId5", SongId = GetById, Text="comment5", UserId = Users[1].Id, CommentsChildren = new List<Comment>(), ParentCommentId = null, CommentReactions = new List<CommentReaction>()},
        };

        public static SongEditModel UpdateModel => new SongEditModel()
        {
            Name = "asd",
            CategoryId = Categories[0].Id,
            Text = "this is test text",
        };
    }
}
