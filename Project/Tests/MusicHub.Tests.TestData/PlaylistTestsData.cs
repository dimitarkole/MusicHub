namespace MusicHub.Tests.TestData
{
    using MusicHub.Common;
    using MusicHub.Data.Models;
    using MusicHub.Web.ViewModels.PlaylistModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class PlaylistTestsData
    {
        public static PlaylistCreateInputModel CreateModel => new PlaylistCreateInputModel()
        {
            Name = "simple playlist name",
        };

        public static PlaylistEditInputModel UpdateModel => new PlaylistEditInputModel()
        {
            Name = "simple playlist name",
        };

        public static string GetOwnedForAddingMusicMusicId => Musics[0].Id;

        public static string IsOwnedPlaylistTruePlaylistId => Playlists[0].Id;

        public static string IsOwnedPlaylistTrueUserId => Playlists[0].UserId;

        public static string IsOwnedPlaylistFalsePlaylistId => Playlists[0].Id;

        public static string IsOwnedPlaylistFalseUserId => Users
            .Where(u => u.Id != Playlists.Where(s => s.Id == IsOwnedPlaylistFalsePlaylistId)
                .FirstOrDefault().UserId)
            .FirstOrDefault()
            .Id;

        public static string CreateUserId => Users[0].Id;

        public static string AllOwnedUserId => Users[0].Id;

        public static List<Category> Categories => CategoryTestsData.GetCategories;

        public static string DeletePlaylistId => Playlists[0].Id;

        public static string UpdatePlaylistId => Playlists[0].Id;

        public static string GetById => Playlists[0].Id;

        public static PlaylistFilterInputModel FilterModel => new PlaylistFilterInputModel() {
            Name = "1",
            OrderMethod = OrderMethod.CreatedOnAsc,
        };

        public static PlaylistFilterInputModel FilterModelWithUserId => new PlaylistFilterInputModel()
        {
            Name = "1",
            OrderMethod = OrderMethod.CreatedOnAsc,
            UserId = "2",
        };

        public static string FilterModelUserId => Users[0].Id;

        public static List<ApplicationUser> Users => UserTestsData.Users;

        public static List<Song> Musics => SongTestData.GetSongs;

        public static List<PlaylistSong> PlaylistSongs => PlaylistSongTestsData.PlaylistSongs();

        public static List<Playlist> Playlists => new List<Playlist>()
        {
            new Playlist(){ Id = "PlaylistId1", Name="playlist name1", UserId = Users[0].Id, VisibleStatus = VisibleStatus.Public,  PlaylistSongs = new List<PlaylistSong>()},
            new Playlist(){ Id = "PlaylistId2", Name="playlist name2", UserId = Users[1].Id, VisibleStatus = VisibleStatus.Public,  PlaylistSongs = new List<PlaylistSong>()},
            new Playlist(){ Id = "PlaylistId3", Name="playlist name3", UserId = Users[2].Id, VisibleStatus = VisibleStatus.Public,  PlaylistSongs = new List<PlaylistSong>()},
            new Playlist(){ Id = "PlaylistId4", Name="playlist name4", UserId = Users[1].Id, VisibleStatus = VisibleStatus.Public,  PlaylistSongs = new List<PlaylistSong>()},
            new Playlist(){ Id = "PlaylistId5", Name="playlist name5", UserId = Users[1].Id, VisibleStatus = VisibleStatus.Public,  PlaylistSongs = new List<PlaylistSong>()},
            new Playlist(){ Id = "PlaylistId6", Name="playlist name6", UserId = Users[2].Id, VisibleStatus = VisibleStatus.Public,  PlaylistSongs = new List<PlaylistSong>()},
            new Playlist(){ Id = "PlaylistId7", Name="playlist name7", UserId = Users[3].Id, VisibleStatus = VisibleStatus.Public,  PlaylistSongs = new List<PlaylistSong>()},
            new Playlist(){ Id = "PlaylistId 8", Name="playlist name8", UserId = Users[1].Id, VisibleStatus = VisibleStatus.Public,  PlaylistSongs = new List<PlaylistSong>()},
            new Playlist(){ Id = "PlaylistId 9", Name="playlist name9", UserId = Users[1].Id, VisibleStatus = VisibleStatus.Public,  PlaylistSongs = new List<PlaylistSong>()},
            new Playlist(){ Id = "PlaylistId 10", Name="playlist name10", UserId = Users[2].Id, VisibleStatus = VisibleStatus.Public,  PlaylistSongs = new List<PlaylistSong>()},
            new Playlist(){ Id = "PlaylistId 11", Name="playlist name11", UserId = Users[1].Id, VisibleStatus = VisibleStatus.Public,  PlaylistSongs = new List<PlaylistSong>()},
            new Playlist(){ Id = "PlaylistId 12", Name="playlist name12", UserId = Users[1].Id, VisibleStatus = VisibleStatus.Public,  PlaylistSongs = new List<PlaylistSong>()},
            new Playlist(){ Id = "PlaylistId 13", Name="playlist name13", UserId = Users[1].Id, VisibleStatus = VisibleStatus.Public,  PlaylistSongs = new List<PlaylistSong>()},
            new Playlist(){ Id = "PlaylistId 14", Name="playlist name14", UserId = Users[2].Id, VisibleStatus = VisibleStatus.Public,  PlaylistSongs = new List<PlaylistSong>()},
            new Playlist(){ Id = "PlaylistId 15", Name="playlist name15", UserId = Users[2].Id, VisibleStatus = VisibleStatus.Public,  PlaylistSongs = new List<PlaylistSong>()},
            new Playlist(){ Id = "PlaylistId 16", Name="playlist name16", UserId = Users[1].Id, VisibleStatus = VisibleStatus.Public,  PlaylistSongs = new List<PlaylistSong>()},
            new Playlist(){ Id = "PlaylistId 17", Name="playlist name17", UserId = Users[0].Id, VisibleStatus = VisibleStatus.Public,  PlaylistSongs = new List<PlaylistSong>()},
        };

    }
}
