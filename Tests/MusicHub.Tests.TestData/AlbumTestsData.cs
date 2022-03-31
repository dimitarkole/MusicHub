namespace MusicHub.Tests.TestData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using MusicHub.Common;
    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.Web.ViewModels.AlbumModels;

    public class AlbumTestsData
    {
        public static AlbumCreateInputModel CreateModel => new AlbumCreateInputModel()
        {
            Name = "simple album name",
            ImageUrl = "Image url.jpg",
            Privacy = Privacy.Protected,
        };

        public static AlbumCreateInputModel CreateEmptyModel => new AlbumCreateInputModel()
        {
            Name = "",
            ImageUrl = "",
            Privacy = Privacy.Protected,
        };

        public static AlbumEditInputModel UpdateModel => new AlbumEditInputModel()
        {
            Name = "simple album name",
            ImageUrl = "Image url.jpg",
            Privacy = Privacy.Protected,
        };

        public static AlbumEditInputModel UpdateEmptyModel => new AlbumEditInputModel()
        {
            Name = "",
            ImageUrl = "",
            Privacy = Privacy.Protected,
        };

        public static string GetOwnedForAddingMusicMusicId => Musics[0].Id;

        public static string IsOwnedAlbumTrueAlbumId => Albums[0].Id;

        public static string IsOwnedAlbumTrueUserId => Albums[0].UserId;

        public static string IsOwnedAlbumFalseAlbumId => Albums[0].Id;

        public static string IsOwnedAlbumFalseUserId => Users
            .Where(u => u.Id != Albums.Where(s => s.Id == IsOwnedAlbumFalseAlbumId)
                .FirstOrDefault().UserId)
            .FirstOrDefault()
            .Id;

        public static string CreateUserId => Users[0].Id;

        public static string AllOwnedUserId => Users[0].Id;

        public static List<Category> Categories => CategoryTestsData.GetCategories;

        public static string DeleteAlbumId => Albums[0].Id;

        public static string UpdateAlbumId => Albums[0].Id;

        public static string GetById => Albums[0].Id;

        public static AlbumFilterInputModel FilterModel => new AlbumFilterInputModel()
        {
            Name = "1",
            OrderMethod = OrderMethod.CreatedOnAsc,
        };

        public static AlbumFilterInputModel FilterModelWithUserId => new AlbumFilterInputModel()
        {
            Name = "1",
            OrderMethod = OrderMethod.CreatedOnAsc,
            UserId = "2",
        };

        public static string FilterModelUserId => Users[0].Id;

        public static List<User> Users => UserTestsData.Users;

        public static List<Music> Musics => MusicTestsData.GetMusics;

        public static List<AlbumMusic> AlbumMusics => AlbumMusicTestsData.AlbumMusics();

        public static List<Album> Albums => new List<Album>()
        {
            new Album() { Id = "AlbumId1", ImageUrl = "Image url.jpg", Name = "album name1", UserId = Users[0].Id, Privacy = Privacy.Public,  AlbumMusics = new List<AlbumMusic>() },
            new Album() { Id = "AlbumId2", ImageUrl = "Image url.jpg", Name = "album name2", UserId = Users[1].Id, Privacy = Privacy.Public,  AlbumMusics = new List<AlbumMusic>() },
            new Album() { Id = "AlbumId3", ImageUrl = "Image url.jpg", Name = "album name3", UserId = Users[2].Id, Privacy = Privacy.Public,  AlbumMusics = new List<AlbumMusic>() },
            new Album() { Id = "AlbumId4", ImageUrl = "Image url.jpg", Name = "album name4", UserId = Users[1].Id, Privacy = Privacy.Public,  AlbumMusics = new List<AlbumMusic>() },
            new Album() { Id = "AlbumId5", ImageUrl = "Image url.jpg", Name = "album name5", UserId = Users[1].Id, Privacy = Privacy.Public,  AlbumMusics = new List<AlbumMusic>() },
            new Album() { Id = "AlbumId6", ImageUrl = "Image url.jpg", Name = "album name6", UserId = Users[2].Id, Privacy = Privacy.Public,  AlbumMusics = new List<AlbumMusic>() },
            new Album() { Id = "AlbumId7", ImageUrl = "Image url.jpg", Name = "album name7", UserId = Users[3].Id, Privacy = Privacy.Public,  AlbumMusics = new List<AlbumMusic>() },
            new Album() { Id = "AlbumId8", ImageUrl = "Image url.jpg", Name = "album name8", UserId = Users[1].Id, Privacy = Privacy.Public,  AlbumMusics = new List<AlbumMusic>() },
            new Album() { Id = "AlbumId9", ImageUrl = "Image url.jpg", Name = "album name9", UserId = Users[1].Id, Privacy = Privacy.Public,  AlbumMusics = new List<AlbumMusic>() },
            new Album() { Id = "AlbumId10", ImageUrl = "Image url.jpg", Name = "album name10", UserId = Users[2].Id, Privacy = Privacy.Public,  AlbumMusics = new List<AlbumMusic>() },
            new Album() { Id = "AlbumId11", ImageUrl = "Image url.jpg", Name = "album name11", UserId = Users[1].Id, Privacy = Privacy.Public,  AlbumMusics = new List<AlbumMusic>() },
            new Album() { Id = "AlbumId12", ImageUrl = "Image url.jpg", Name = "album name12", UserId = Users[1].Id, Privacy = Privacy.Public,  AlbumMusics = new List<AlbumMusic>() },
            new Album() { Id = "AlbumId13", ImageUrl = "Image url.jpg", Name = "album name13", UserId = Users[1].Id, Privacy = Privacy.Public,  AlbumMusics = new List<AlbumMusic>() },
            new Album() { Id = "AlbumId14", ImageUrl = "Image url.jpg", Name = "album name14", UserId = Users[2].Id, Privacy = Privacy.Public,  AlbumMusics = new List<AlbumMusic>() },
            new Album() { Id = "AlbumId15", ImageUrl = "Image url.jpg", Name = "album name15", UserId = Users[2].Id, Privacy = Privacy.Public,  AlbumMusics = new List<AlbumMusic>() },
            new Album() { Id = "AlbumId16", ImageUrl = "Image url.jpg", Name = "album name16", UserId = Users[1].Id, Privacy = Privacy.Public,  AlbumMusics = new List<AlbumMusic>() },
            new Album() { Id = "AlbumId17", ImageUrl = "Image url.jpg", Name = "album name17", UserId = Users[0].Id, Privacy = Privacy.Public,  AlbumMusics = new List<AlbumMusic>() },
        };
    }
}
