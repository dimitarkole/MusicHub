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

namespace MusicHub.Web.Tests.Controllers
{
    public class HomeControllerTests
    {
        [Fact]
        public void All_WithDataInTheDb_ShouldReturnOkWithCorrectData()
        {
            var musics = MusicTestsData.GetMusics;
            var expected = musics[0]
                .To<MusicDetailModel>();

            MyController<HomeController>
                .Instance()
                .WithData(musics)
                .Calling(c => c.Get(expected.Id))
                .ShouldReturn()
                .Ok(result => result
                    .WithModelOfType<MusicDetailModel>()
                    .Passing(actual=>
                    {
                        expected.Should().BeEquivalentTo(actual);
                    }));
        }
    }
}
