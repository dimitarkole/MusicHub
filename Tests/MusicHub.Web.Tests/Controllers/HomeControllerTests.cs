using MusicHub.Tests.TestData;
using MusicHub.Web.Controllers;
using MusicHub.Web.ViewModels.MusicModels;
using MyTested.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using MusicHub.Services.Mapping;
using FluentAssertions;
using MusicHub.Services.Data;
using MusicHub.Services.Data.Interfaces;
using MusicHub.Data;
using MusicHub.Services.Interfaces;
using static MusicHub.Common.GlobalConstants;
using MusicHub.Web.ViewModels.PlaylistModels;

namespace MusicHub.Web.Tests.Controllers
{
    public class HomeControllerTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetAllMusics_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var musics = MusicTestsData.GetMusics;
            var categories = MusicTestsData.Categories;

            MyController<HomeController>
                .Instance()
                .WithData(musics)
                .WithData(categories)
                .Calling(c => c.Get(page))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<MusicsAllViewModel<MusicViewModel>>()
                    .Passing(actual=>
                    {
                        var entitesPerPage = PaginationData.MusicsPerPage;
                        var service = new MusicService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.All<MusicViewModel>(page, entitesPerPage);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Fact]
        public void GetMusicById_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var musics = MusicTestsData.GetMusics;
            var users = MusicTestsData.Users;
            var categories = MusicTestsData.Categories;
            var musicId = musics[0].Id;

            MyController<HomeController>
                .Instance()
                .WithData(users)
                .WithData(musics)
                .WithData(categories)
                .Calling(c => c.Get(musicId))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<MusicDetailModel>()
                    .Passing(actual =>
                    {
                        var service = new MusicService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.GetById<MusicDetailModel>(musicId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void FilterMusics_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var musics = MusicTestsData.GetMusics;
            var categories = MusicTestsData.Categories;
            var filter = MusicTestsData.FilterModel;
            MyController<HomeController>
                .Instance()
                .WithData(musics)
                .WithData(categories)
                .Calling(c => c.Filter(page, filter))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<MusicsAllViewModel<MusicViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.MusicsPerPage;
                        var service = new MusicService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.Filter<MusicViewModel>(page, entitesPerPage, filter);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void FilterMusicsWhitUserId_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var musics = MusicTestsData.GetMusics;
            var categories = MusicTestsData.Categories;
            var filter = MusicTestsData.FilterModelWhitUserId;
            MyController<HomeController>
                .Instance()
                .WithData(musics)
                .WithData(categories)
                .Calling(c => c.Filter(page, filter))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<MusicsAllViewModel<MusicViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.MusicsPerPage;
                        var service = new MusicService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.Filter<MusicViewModel>(page, entitesPerPage, filter);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void FilterMusicsWhitUserIdAndCategoryId_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var musics = MusicTestsData.GetMusics;
            var categories = MusicTestsData.Categories;
            var filter = MusicTestsData.FilterModelWhitUserIdAndCategoryId;
            MyController<HomeController>
                .Instance()
                .WithData(musics)
                .WithData(categories)
                .Calling(c => c.Filter(page, filter))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<MusicsAllViewModel<MusicViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.MusicsPerPage;
                        var service = new MusicService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.Filter<MusicViewModel>(page, entitesPerPage, filter);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void FilterMusicsWhitUserName_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var musics = MusicTestsData.GetMusics;
            var categories = MusicTestsData.Categories;
            var filter = MusicTestsData.FilterModelWhitUserName;
            MyController<HomeController>
                .Instance()
                .WithData(musics)
                .WithData(categories)
                .Calling(c => c.Filter(page, filter))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<MusicsAllViewModel<MusicViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.MusicsPerPage;
                        var service = new MusicService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.Filter<MusicViewModel>(page, entitesPerPage, filter);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void FilterMusicsWhitCategory_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var musics = MusicTestsData.GetMusics;
            var categories = MusicTestsData.Categories;
            var filter = MusicTestsData.FilterModelWithCategory;
            MyController<HomeController>
                .Instance()
                .WithData(musics)
                .WithData(categories)
                .Calling(c => c.Filter(page, filter))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<MusicsAllViewModel<MusicViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.MusicsPerPage;
                        var service = new MusicService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.Filter<MusicViewModel>(page, entitesPerPage, filter);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void SuggestMusics_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var musics = MusicTestsData.GetMusics;
            var categories = MusicTestsData.Categories;
            var musicId = MusicTestsData.SuggestMusic.Id;

            MyController<HomeController>
                .Instance()
                .WithData(musics)
                .WithData(categories)
                .Calling(c => c.SuggestMusics(page, musicId))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<MusicsAllViewModel<MusicViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.SuggestMusicPerPage;
                        var service = new MusicService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.SuggestMusics<MusicViewModel>(page, entitesPerPage, musicId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetAllPlaylists_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var playlists = PlaylistTestsData.Playlists;
            var musics = PlaylistTestsData.Musics;
            var categories = PlaylistTestsData.Categories;

            MyController<HomeController>
                .Instance()
                .WithData(playlists)
                .WithData(musics)
                .WithData(categories)
                .Calling(c => c.GetPlaylists(page))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<PlaylistsAllViewModel<PlaylistViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.MusicsPerPage;
                        var service = new PlaylistService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.All<PlaylistViewModel>(page, entitesPerPage);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void FilterPlaylists_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var playlists = PlaylistTestsData.Playlists;
            var musics = PlaylistTestsData.Musics;
            var categories = PlaylistTestsData.Categories;
            var filter = PlaylistTestsData.FilterModel;

            MyController<HomeController>
                .Instance()
                .WithData(playlists)
                .WithData(musics)
                .WithData(categories)
                .Calling(c => c.PlaylistFilter(page, filter))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<PlaylistsAllViewModel<PlaylistViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.MusicsPerPage;
                        var service = new PlaylistService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.Filter<PlaylistViewModel>(page, entitesPerPage, filter);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void FilterPlaylistsWithUserId_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var playlists = PlaylistTestsData.Playlists;
            var musics = PlaylistTestsData.Musics;
            var categories = PlaylistTestsData.Categories;
            var filter = PlaylistTestsData.FilterModelWithUserId;

            MyController<HomeController>
                .Instance()
                .WithData(playlists)
                .WithData(musics)
                .WithData(categories)
                .Calling(c => c.PlaylistFilter(page, filter))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<PlaylistsAllViewModel<PlaylistViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.MusicsPerPage;
                        var service = new PlaylistService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.Filter<PlaylistViewModel>(page, entitesPerPage, filter);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }


        [Fact]
        public void GetPlaylistById_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var playlists = PlaylistTestsData.Playlists;
            var musics = PlaylistTestsData.Musics;
            var categories = PlaylistTestsData.Categories;
            var playlistId = playlists[0].Id;

            MyController<HomeController>
                .Instance()
                .WithData(playlists)
                .WithData(musics)
                .WithData(categories)
                .Calling(c => c.GetPlaylit(playlistId))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<PlaylistPlayModel>()
                    .Passing(actual =>
                    {
                        var service = new PlaylistService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.GetById<PlaylistPlayModel>(playlistId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }
    }
}
