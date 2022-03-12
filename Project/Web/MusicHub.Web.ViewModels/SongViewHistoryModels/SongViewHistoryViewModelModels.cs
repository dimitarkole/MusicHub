namespace MusicHub.Web.ViewModels.SongViewHistoryModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;
    using MusicHub.Web.ViewModels.SongModels;

    public class SongViewHistoryViewModelModels : IMapFrom<SongViewHistory>
    {
        public string Id { get; set; }

        public virtual SongViewModel Song { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
