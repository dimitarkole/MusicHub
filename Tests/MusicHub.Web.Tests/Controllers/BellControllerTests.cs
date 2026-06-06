using FluentAssertions;
using Microsoft.AspNetCore.Http;
using MusicHub.Data;
using MusicHub.Services.Data;
using MusicHub.Services.Interfaces;
using MusicHub.Tests.TestData;
using MusicHub.Web.Controllers;
using MusicHub.Web.ViewModels.BellModels;
using MyTested.AspNetCore.Mvc;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using Xunit;
using static MusicHub.Common.GlobalConstants;

namespace MusicHub.Web.Tests.Controllers
{
    public class BellControllerTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetOwnBelling_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var users = BellTestsData.Users;
            var bells = BellTestsData.Bells();
            var userId = users[0].Id;

            MyController<BellController>
                .Instance()
                .WithData(users)
                .WithData(bells)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.GetOwnBelling(page))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<BellingPageModel<BellingViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.BellsPerPage;
                        var service = new BellService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.AllBellings<BellingViewModel>(page, entitesPerPage, userId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetBelling_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var users = BellTestsData.Users;
            var bells = BellTestsData.Bells();
            var userId = users[0].Id;

            MyController<BellController>
                .Instance()
                .WithData(users)
                .WithData(bells)
                .Calling(c => c.GetBelling(userId, page))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<BellingPageModel<BellingViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.BellsPerPage;
                        var service = new BellService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.AllBellings<BellingViewModel>(page, entitesPerPage, userId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetBellers_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var users = BellTestsData.Users;
            var bells = BellTestsData.Bells();
            var userId = users[0].Id;

            MyController<BellController>
                .Instance()
                .WithData(users)
                .WithData(bells)
                .Calling(c => c.GetBellers(userId, page))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<BellerPageModel<BellerViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.BellsPerPage;
                        var service = new BellService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.AllBellers<BellerViewModel>(page, entitesPerPage, userId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void FilterBellers_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var users = BellTestsData.Users;
            var bells = BellTestsData.Bells();
            var userId = users[0].Id;
            var filter = BellTestsData.FilterModelFirstName;
            MyController<BellController>
                .Instance()
                .WithData(users)
                .WithData(bells)
                .Calling(c => c.FilterBellers(userId, page, filter))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<BellerPageModel<BellerViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.BellsPerPage;
                        var service = new BellService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.FilterBellers<BellerViewModel>(page, entitesPerPage, filter, userId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void FilterBellings_WithDataInTheDb_ShouldReturnOkWithCorrectData(int page)
        {
            var users = BellTestsData.Users;
            var bells = BellTestsData.Bells();
            var userId = users[0].Id;
            var filter = BellTestsData.FilterModelFirstName;

            MyController<BellController>
                .Instance()
                .WithData(users)
                .WithData(bells)
                .Calling(c => c.FilterBellings(userId, page, filter))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<BellingPageModel<BellingViewModel>>()
                    .Passing(actual =>
                    {
                        var entitesPerPage = PaginationData.BellsPerPage;
                        var service = new BellService(From.Services<ApplicationDbContext>(), From.Services<IPaginationService>());
                        var expected = service.FilterBellings<BellingViewModel>(page, entitesPerPage, filter, userId);
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }

        [Fact]
        public void Create_WhitCurrectData_ShouldReturnOk()
        {
            var users = BellTestsData.Users;
            var model = new BellCreateInputModel()
            {
                BellingId = users[1].Id,
            };
            var userId = users[0].Id;

            MyController<BellController>
                .Instance()
                .WithData(users)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Post(model))
                .ShouldReturn()
                .StatusCode(StatusCodes.Status201Created);

            Assert.True(From.Services<ApplicationDbContext>().Bells
                             .Any(f => f.BellerId == userId
                                && f.BellingId == model.BellingId));
        }

        [Fact]
        public void Create_WhitCurrectData_UserBellThem_ShouldReturnBadRequest()
        {
            var users = BellTestsData.Users;
            var model = new BellCreateInputModel()
            {
                BellingId = users[0].Id,
            };
            var userId = users[0].Id;

            MyController<BellController>
                .Instance()
                .WithData(users)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Post(model))
                .ShouldReturn()
                .BadRequest();

            Assert.False(From.Services<ApplicationDbContext>().Bells
                             .Any(f => f.BellerId == userId
                                && f.BellingId == model.BellingId));
        }

        [Fact]
        public void Create_WhitCurrectData_UserBellUserАgain_ShouldReturnBadRequest()
        {
            var users = BellTestsData.Users;
            var bells = BellTestsData.Bells();
            var model = new BellCreateInputModel()
            {
                BellingId = bells[0].BellingId,
            };
            var userId = users[0].Id;

            MyController<BellController>
                .Instance()
                .WithData(users)
                .WithData(bells)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Post(model))
                .ShouldReturn()
                .BadRequest();

            Assert.Equal(1, From.Services<ApplicationDbContext>().Bells
                             .Count(f => f.BellerId == userId
                                && f.BellingId == model.BellingId));
        }

        [Fact]
        public void IsBelling_WhitCurrectData_ShouldReturnFalse()
        {
            var users = BellTestsData.Users;
            var bells = BellTestsData.Bells();
            var userId = users[0].Id;
            var bellingId = bells
                .Where(f => f.BellerId != userId)
                .FirstOrDefault()
                .BellingId;
            MyController<BellController>
                .Instance()
                .WithData(users)
                .WithData(bells)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.IsBelling(bellingId))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<bool>()
                    .Passing(actual =>
                    {
                        Assert.False(actual);
                    }));
        }

        [Fact]
        public void IsBelling_WhitCurrectData_ShouldReturnTrue()
        {
            var users = BellTestsData.Users;
            var bells = BellTestsData.Bells();
            var userId = users[0].Id;
            var bellingId = bells
                .Where(f => f.BellerId == userId)
                .FirstOrDefault()
                .BellingId;
            MyController<BellController>
                .Instance()
                .WithData(users)
                .WithData(bells)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.IsBelling(bellingId))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<bool>()
                    .Passing(actual =>
                    {
                        Assert.True(actual);
                    }));
        }

        [Fact]
        public void GetBellingId_WhitCurrectData_ShouldReturnNull()
        {
            var users = BellTestsData.Users;
            var bells = BellTestsData.Bells();
            var userId = users[0].Id;

            var bellingId = bells
                .Where(f => f.BellerId != userId)
                .FirstOrDefault()
                .BellingId;
            MyController<BellController>
                .Instance()
                .WithData(users)
                .WithData(bells)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.GetBellingId(bellingId))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<object>()
                    .Passing(obj =>
                    {
                        foreach (PropertyInfo property in obj.GetType().GetProperties())
                        {
                            if (property.Name == "Id")
                            {
                                Assert.Empty(property.GetValue(obj).ToString());
                            }
                        }
                    }));
        }

        [Fact]
        public void GetBellingId_WhitCurrectData_ShouldReturnOkWithBellId()
        {
            var users = BellTestsData.Users;
            var bells = BellTestsData.Bells();
            var userId = users[0].Id;
            var bell = bells
               .Where(f => f.BellerId == userId)
               .FirstOrDefault();
            var bellingId = bell.BellingId;
            MyController<BellController>
                .Instance()
                .WithData(users)
                .WithData(bells)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.GetBellingId(bellingId))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<object>()
                    .Passing(obj =>
                    {
                        foreach (PropertyInfo property in obj.GetType().GetProperties())
                        {
                            if (property.Name == "Id")
                            {
                                Assert.Equal(bell.Id, property.GetValue(obj).ToString());
                            }
                        }
                    }));
        }

        [Fact]
        public void DeleteBell_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var users = BellTestsData.Users;
            var bells = BellTestsData.Bells();
            var userId = users[0].Id;
            var deleteBellId = bells.Where(f => f.BellerId == userId)
                .FirstOrDefault()
                .Id;
            MyController<BellController>
                .Instance()
                .WithData(users)
                .WithData(bells)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Delete(deleteBellId))
                .ShouldReturn()
                .Ok();

            Assert.False(From.Services<ApplicationDbContext>().Bells
                .Any(f => f.Id == deleteBellId));
        }

        [Fact]
        public void DeleteNotOwnedBell_WithDataInTheDb_ShouldReturnForbidWithCorrectData()
        {
            var users = BellTestsData.Users;
            var bells = BellTestsData.Bells();
            var userId = users[0].Id;
            var deleteBellId = bells.Where(f => f.BellerId != userId)
                .FirstOrDefault()
                .Id;
            MyController<BellController>
                .Instance()
                .WithData(users)
                .WithData(bells)
                .WithUser(user => user
                    .WithClaim(ClaimTypes.NameIdentifier, userId))
                .Calling(c => c.Delete(deleteBellId))
                .ShouldReturn()
                .Forbid();

            Assert.True(From.Services<ApplicationDbContext>().Bells
                .Any(f => f.Id == deleteBellId));
        }
    }
}
