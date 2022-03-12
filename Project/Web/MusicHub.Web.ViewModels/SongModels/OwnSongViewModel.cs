namespace MusicHub.Web.ViewModels.SongModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class OwnSongViewModel : IMapFrom<Song>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public string Path { get; set; }

        public string MusicCategoryId { get; set; }

        public string MusicCategoryName { get; set; }

        public string Text { get; set; }

        // public int Rating { get; set; }

        public long CountLikes { get; set; }

        public long CountViews { get; set; }

        public long CountDisLikes { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
           /* configuration
               .CreateMap<Song, OwnSongViewModel>()
               .ForMember(
                   destination => destination.Rating,
                   opts => opts.MapFrom(origin =>
                        origin.SongLikes.Count + origin.SongDislikes.Count == 0 ? 0 :
                            (int)((origin.SongLikes.Count * 1.0 / (origin.SongLikes.Count + origin.SongDislikes.Count)) * 100)));
            
            configuration.CreateMap<Song, OwnSongViewModel>()
                            .ForMember(s => s.Rating, y => y.MapFrom(m => m.CountLikes + m.CountDisLikes != 0 ? m.CountLikes / (m.CountLikes + m.CountDisLikes) : 0));
            */// / (s.CountLikes + s.CountDisLikes), y => y.MapFrom(m => m.Rating = ));
        }
    }
}
