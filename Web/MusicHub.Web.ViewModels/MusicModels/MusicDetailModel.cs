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

        public string Privacy { get; set; }

        public string BuyLink { get; set; }

        public string AlbumTitle { get; set; }

        public string RecordLabel { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string Barcode { get; set; }

        public string ISWC { get; set; }

        public string PLine { get; set; }

        public string ContainsExplicitContent { get; set; }

        public bool EnableDirectDownloads { get; set; }

        public bool DisplayEmbedCode { get; set; }

        public bool OfflineListening { get; set; }

        public bool AppPlayback { get; set; }

        public bool RSSfeed { get; set; }

        public string ContainsMusic { get; set; }

        public string Artist { get; set; }

        public string Publisher { get; set; }

        public string ISRC { get; set; }

        public string Composer { get; set; }

        public string ReleaseTitle { get; set; }

        public string License { get; set; }

        public bool Attribution { get; set; }

        public bool Noncommercial { get; set; }

        public bool NoDerivativeWorks { get; set; }

        public bool ShareALike { get; set; }

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
