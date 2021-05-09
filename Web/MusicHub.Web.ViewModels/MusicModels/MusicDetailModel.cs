namespace MusicHub.Web.ViewModels.MusicModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.Services.Mapping;
    using MusicHub.Web.ViewModels.MusicLicenseModels;

    public class MusicDetailModel : IMapFrom<Music>, IHaveCustomMappings
    {

        public string Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string AudioUrl { get; set; }

        public virtual string CategoryId { get; set; }

        public virtual string CategoryName { get; set; }

        public string Description { get; set; }

        public int CountLikes { get; set; }

        public int CountDislikes { get; set; }

        public DateTime CreatedOn { get; set; }

        public MusicUserViewModel User { get; set; }

        public MusicLicenseType MusicLicenseType { get; set; }

        public VisibleStatus VisibleStatus { get; set; }

        public virtual ICollection<MusicLicenseViewModel> MusicLicenses { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Music, MusicDetailModel>()
                .ForMember(
                    m => m.CountLikes,
                    y => y.MapFrom(
                        s => s.MusicReactions.Where(
                            r => r.Reaction == Reaction.Like)
                        .Count()))
              .ForMember(
                  m => m.CountDislikes,
                  y => y.MapFrom(
                      s => s.MusicReactions.Where(
                          r => r.Reaction == Reaction.Dislike)
                      .Count()));
        }
    }
}
