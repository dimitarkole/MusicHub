using MusicHub.Common;
using MusicHub.Data.Models;
using MusicHub.Data.Models.Enums;
using MusicHub.Web.ViewModels.MusicModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicHub.Tests.TestData
{
    public static class MusicTestsData
    {
        public static MusicCreateInputModel CreateModel => new MusicCreateInputModel()
        {
            Name = "music name",
            CategoryId = Categories[0].Id,
            ImageUrl = "image path.jpg",
            AudioUrl = "audio path.mp3",
            MusicLicenseType = MusicLicenseType.CC,
            VisibleStatus = VisibleStatus.Public,
            Description = "this is test description",
        };

        public static MusicEditModel UpdateModel => new MusicEditModel()
        {
            Name = "music name edit",
            CategoryId = Categories[1].Id,
            ImageUrl = "image path edit.jpg",
            AudioUrl = "audio path edit.mp3",
            MusicLicenseType = MusicLicenseType.CCBY,
            VisibleStatus = VisibleStatus.Hidden,
            Description = "this is test description edit",
        };

        public static string UpdateMusicId => GetMusics[0].Id;

        public static string CreateUserId => Users[0].Id;

        public static string DeleteMusicId => GetMusics[0].Id;

        public static List<Category> Categories => CategoryTestsData.GetCategories;

        public static List<User> Users => UserTestsData.Users;

        public static string GetOwnedUserId => Users[0].Id;

        public static string IsOwnedTrueMusicId => GetMusics[0].Id;

        public static string IsOwedMusicTrueUserId => GetMusics[0].UserId;

        public static string IsOwnedFalseMusicId => GetMusics[0].Id;

        public static string IsOwnedMusicFalseUserId => Users
            .Where(u => u.Id != GetMusics.Where(s => s.Id == IsOwnedFalseMusicId)
                .FirstOrDefault().UserId)
            .FirstOrDefault()
            .Id;

        public static string GetById => GetMusics[0].Id;

        public static MusicFilter FilterModelWithCategory => new MusicFilter
        {
            CategoryId = Categories[0].Id,
            Name = "1",
            OrderMethod = OrderMethod.CreatedOnAsc,
        };

        public static MusicFilter FilterModel => new MusicFilter
        {
            Name = "1",
            OrderMethod = OrderMethod.CreatedOnAsc,
        };

        public static MusicFilter FilterModelWhitUserId => new MusicFilter
        {
            Name = "Mus",
            UserId = Users[0].Id,
            OrderMethod = OrderMethod.CreatedOnAsc,
        };

        public static MusicFilter FilterModelWhitUserIdAndCategoryId => new MusicFilter
        {
            CategoryId = Categories[0].Id,
            Name = "Mus",
            UserId = Users[0].Id,
            OrderMethod = OrderMethod.CreatedOnAsc,
        };

        public static MusicFilter FilterModelWhitUserName => new MusicFilter
        {
            CategoryId = Categories[0].Id,
            Name = "1",
            UserId = Users[0].Id,
            OrderMethod = OrderMethod.CreatedOnAsc,
            Username = Users[0].UserName,
        };

        public static string FilterUserId => GetMusics[0].Id;

        public static Music SuggestMusic => GetMusics[0];

        public static List<Music> GetMusics => new List<Music>()
        {
            new Music(){ Id ="testMusicId1", Name = "testMusic1", CategoryId = Categories[0].Id, Description = "some Description", UserId = Users[0].Id, AudioUrl="audio.mp3", ImageUrl = "image.jpg", MusicReactions = new List<MusicReaction>(), PlaylistMusics = new List<PlaylistMusic>(), PlayedMusics= new List<PlayedMusic>(), Comments = new List<Comment>(), MusicLicenseType = MusicLicenseType.CC, MusicLicenses = new List<MusicLicense>(), VisibleStatus = VisibleStatus.Public } ,
            new Music(){ Id ="testMusicId2", Name = "testMusic2", CategoryId = Categories[1].Id, Description = "some Description", UserId = Users[1].Id, AudioUrl="audio.mp3", ImageUrl = "image.jpg", Comments = new List<Comment>(), MusicReactions = new List<MusicReaction>(), PlaylistMusics = new List<PlaylistMusic>(), PlayedMusics= new List<PlayedMusic>(), MusicLicenseType = MusicLicenseType.CC, MusicLicenses = new List<MusicLicense>(), VisibleStatus = VisibleStatus.Public },
            new Music(){ Id ="testMusicId3", Name = "testMusic3", CategoryId = Categories[0].Id, Description = "some Description", UserId = Users[1].Id, AudioUrl="audio.mp3", ImageUrl = "image.jpg", Comments = new List<Comment>(), MusicReactions = new List<MusicReaction>(), PlaylistMusics = new List<PlaylistMusic>(), PlayedMusics= new List<PlayedMusic>(), MusicLicenseType = MusicLicenseType.CC, MusicLicenses = new List<MusicLicense>(), VisibleStatus = VisibleStatus.Hidden },
            new Music(){ Id ="testMusicId4", Name = "testMusic4", CategoryId = Categories[1].Id, Description = "some Description", UserId = Users[0].Id, AudioUrl="audio.mp3", ImageUrl = "image.jpg", Comments = new List<Comment>(), MusicReactions = new List<MusicReaction>(), PlaylistMusics = new List<PlaylistMusic>(), PlayedMusics= new List<PlayedMusic>(), MusicLicenseType = MusicLicenseType.CC, MusicLicenses = new List<MusicLicense>(), VisibleStatus = VisibleStatus.Public },
            new Music(){ Id ="testMusicId5", Name = "testMusic5", CategoryId = Categories[1].Id, Description = "some Description", UserId = Users[2].Id, AudioUrl="audio.mp3", ImageUrl = "image.jpg", Comments = new List<Comment>(), MusicReactions = new List<MusicReaction>(), PlaylistMusics = new List<PlaylistMusic>(), PlayedMusics= new List<PlayedMusic>(), MusicLicenseType = MusicLicenseType.CC, MusicLicenses = new List<MusicLicense>(), VisibleStatus = VisibleStatus.OnlyMe },
            new Music(){ Id ="testMusicId6", Name = "testMusic6", CategoryId = Categories[1].Id, Description = "some Description", UserId = Users[3].Id, AudioUrl="audio.mp3", ImageUrl = "image.jpg", Comments = new List<Comment>(), MusicReactions = new List<MusicReaction>(), PlaylistMusics = new List<PlaylistMusic>(), PlayedMusics= new List<PlayedMusic>(), MusicLicenseType = MusicLicenseType.CC, MusicLicenses = new List<MusicLicense>(), VisibleStatus = VisibleStatus.Public },
            new Music(){ Id ="testMusicId7", Name = "testMusic7", CategoryId = Categories[1].Id, Description = "some Description", UserId = Users[0].Id, AudioUrl="audio.mp3", ImageUrl = "image.jpg", Comments = new List<Comment>(), MusicReactions = new List<MusicReaction>(), PlaylistMusics = new List<PlaylistMusic>(), PlayedMusics= new List<PlayedMusic>(), MusicLicenseType = MusicLicenseType.CC, MusicLicenses = new List<MusicLicense>(), VisibleStatus = VisibleStatus.Public },
            new Music(){ Id ="testMusicId8", Name = "testMusic8", CategoryId = Categories[1].Id, Description = "some Description", UserId = Users[0].Id, AudioUrl="audio.mp3", ImageUrl = "image.jpg", Comments = new List<Comment>(), MusicReactions = new List<MusicReaction>(), PlaylistMusics = new List<PlaylistMusic>(), PlayedMusics= new List<PlayedMusic>(), MusicLicenseType = MusicLicenseType.CC, MusicLicenses = new List<MusicLicense>(), VisibleStatus = VisibleStatus.OnlyMe },
            new Music(){ Id ="testMusicId9", Name = "testMusic9", CategoryId = Categories[1].Id, Description = "some Description", UserId = Users[1].Id, AudioUrl="audio.mp3", ImageUrl = "image.jpg", Comments = new List<Comment>(), MusicReactions = new List<MusicReaction>(), PlaylistMusics = new List<PlaylistMusic>(), PlayedMusics= new List<PlayedMusic>(), MusicLicenseType = MusicLicenseType.CC, MusicLicenses = new List<MusicLicense>(), VisibleStatus = VisibleStatus.Public },
            new Music(){ Id ="testMusicId10", Name = "testMusic10", CategoryId = Categories[1].Id, Description = "some Description", UserId = Users[0].Id, AudioUrl="audio.mp3", ImageUrl = "image.jpg", Comments = new List<Comment>(), MusicReactions = new List<MusicReaction>(), PlaylistMusics = new List<PlaylistMusic>(), PlayedMusics= new List<PlayedMusic>(), MusicLicenseType = MusicLicenseType.CC, MusicLicenses = new List<MusicLicense>(), VisibleStatus = VisibleStatus.Hidden },
            new Music(){ Id ="testMusicId11", Name = "testMusic11", CategoryId = Categories[1].Id, Description = "some Description", UserId = Users[2].Id, AudioUrl="audio.mp3", ImageUrl = "image.jpg", Comments = new List<Comment>(), MusicReactions = new List<MusicReaction>(), PlaylistMusics = new List<PlaylistMusic>(), PlayedMusics= new List<PlayedMusic>(), MusicLicenseType = MusicLicenseType.CC, MusicLicenses = new List<MusicLicense>(), VisibleStatus = VisibleStatus.Public },
            new Music(){ Id ="testMusicId12", Name = "testMusic12", CategoryId = Categories[1].Id, Description = "some Description", UserId = Users[0].Id, AudioUrl="audio.mp3", ImageUrl = "image.jpg", Comments = new List<Comment>(), MusicReactions = new List<MusicReaction>(), PlaylistMusics = new List<PlaylistMusic>(), PlayedMusics= new List<PlayedMusic>(), MusicLicenseType = MusicLicenseType.CC, MusicLicenses = new List<MusicLicense>(), VisibleStatus = VisibleStatus.Public },
            new Music(){ Id ="testMusicId13", Name = "testMusic13", CategoryId = Categories[1].Id, Description = "some Description", UserId = Users[1].Id, AudioUrl="audio.mp3", ImageUrl = "image.jpg", Comments = new List<Comment>(), MusicReactions = new List<MusicReaction>(), PlaylistMusics = new List<PlaylistMusic>(), PlayedMusics= new List<PlayedMusic>(), MusicLicenseType = MusicLicenseType.CC, MusicLicenses = new List<MusicLicense>(), VisibleStatus = VisibleStatus.Public },
            new Music(){ Id ="testMusicId14", Name = "testMusic14", CategoryId = Categories[2].Id, Description = "some Description", UserId = Users[2].Id, AudioUrl="audio.mp3", ImageUrl = "image.jpg", Comments = new List<Comment>(), MusicReactions = new List<MusicReaction>(), PlaylistMusics = new List<PlaylistMusic>(), PlayedMusics= new List<PlayedMusic>(),  MusicLicenseType = MusicLicenseType.CC, MusicLicenses = new List<MusicLicense>(), VisibleStatus = VisibleStatus.Public },
            new Music(){ Id ="testMusicId15", Name = "testMusic15", CategoryId = Categories[2].Id, Description = "some Description", UserId = Users[2].Id, AudioUrl="audio.mp3", ImageUrl = "image.jpg", Comments = new List<Comment>(), MusicReactions = new List<MusicReaction>(), PlaylistMusics = new List<PlaylistMusic>(), PlayedMusics= new List<PlayedMusic>(),  MusicLicenseType = MusicLicenseType.CC, MusicLicenses = new List<MusicLicense>(), VisibleStatus = VisibleStatus.Public },
            new Music(){ Id ="testMusicId16", Name = "testMusic16", CategoryId = Categories[2].Id, Description = "some Description", UserId = Users[2].Id, AudioUrl="audio.mp3", ImageUrl = "image.jpg", Comments = new List<Comment>(), MusicReactions = new List<MusicReaction>(), PlaylistMusics = new List<PlaylistMusic>(), PlayedMusics= new List<PlayedMusic>(),  MusicLicenseType = MusicLicenseType.CC, MusicLicenses = new List<MusicLicense>(), VisibleStatus = VisibleStatus.Public },
            new Music(){ Id ="testMusicId17", Name = "testMusic17", CategoryId = Categories[2].Id, Description = "some Description", UserId = Users[2].Id, AudioUrl="audio.mp3", ImageUrl = "image.jpg", Comments = new List<Comment>(), MusicReactions = new List<MusicReaction>(), PlaylistMusics = new List<PlaylistMusic>(), PlayedMusics= new List<PlayedMusic>(),  MusicLicenseType = MusicLicenseType.CC, MusicLicenses = new List<MusicLicense>(), VisibleStatus = VisibleStatus.Public },
            new Music(){ Id ="testMusicId18", Name = "testMusic18", CategoryId = Categories[2].Id, Description = "some Description", UserId = Users[2].Id, AudioUrl="audio.mp3", ImageUrl = "image.jpg", Comments = new List<Comment>(), MusicReactions = new List<MusicReaction>(), PlaylistMusics = new List<PlaylistMusic>(), PlayedMusics= new List<PlayedMusic>(),  MusicLicenseType = MusicLicenseType.CC, MusicLicenses = new List<MusicLicense>(), VisibleStatus = VisibleStatus.Public },
        };
    }
}
