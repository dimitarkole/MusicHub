namespace MusicHub.Web.ViewModels.SongViewHistoryModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class SongViewHistoryCreateInputModel : IMapTo<SongViewHistory>
    {
        public string SongId { get; set; }
    }
}
