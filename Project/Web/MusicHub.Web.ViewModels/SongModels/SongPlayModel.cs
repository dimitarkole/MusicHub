namespace MusicHub.Web.ViewModels.SongModels
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;
    using MusicHub.Web.ViewModels.CommentModels;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;
    using NHibernate.Type;
    using MusicHub.Common;

    public class SongPlayModel : IMapFrom<Song>, IHaveCustomMappings
    {

        public string Id { get; set; }

        public string Name { get; set; }

        public string ImageFilePath { get; set; }

        public string AudioFilePath { get; set; }

        public virtual string CategoryId { get; set; }

        public virtual string CategoryName { get; set; }

        public string Text { get; set; }

        public int CountLikes { get; set; }

        public int CountDislikes { get; set; }

        public DateTime CreatedOn { get; set; }

        public SongUserViewModel User { get; set; }

        public MusicLicenseType MusicLicenseType { get; set; }

        public VisibleStatus VisibleStatus { get; set; }

        public virtual ICollection<SongLicense> SongLicenses { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Song, SongPlayModel>()
                .ForMember(
                    m => m.CountLikes,
                    y => y.MapFrom(
                        s => s.SongReactions.Where(
                            r => r.Reaction == Common.Reaction.Like)
                        .Count()))
              .ForMember(
                  m => m.CountDislikes,
                  y => y.MapFrom(
                      s => s.SongReactions.Where(
                          r => r.Reaction == Common.Reaction.Dislike)
                      .Count()));
        }
    }
}
