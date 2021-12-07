namespace MusicHub.Web.ViewModels.MusicModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;
    using MusicHub.Web.ViewModels.CategoryModels;

    public class TopPlayedMusicViewModel : IMapFrom<Music>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string AudioUrl { get; set; }

        public virtual CategoryViewModel Category { get; set; }

        public DateTime CreatedOn { get; set; }

        public int PlayedMusicsCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            var today = DateTime.Now;
            var monthBefore = today.AddMonths(-1);
            configuration.CreateMap<Music, TopPlayedMusicViewModel>()
               .ForMember(
                   m => m.PlayedMusicsCount,
                   y => y.MapFrom(
                       s => s.PlayedMusics.Where(
                           pm => pm.CreatedOn >= monthBefore && pm.CreatedOn <= today)
                       .Count() == 0 ? 0 : s.PlayedMusics.Where(
                           pm => pm.CreatedOn >= monthBefore && pm.CreatedOn <= today)
                       .Count()));
        }
    }
}
