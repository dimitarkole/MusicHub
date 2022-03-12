namespace MusicHub.Web.ViewModels.LicenseMusicModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;
    using MusicHub.Web.ViewModels.LicenseModels;

    public class LicenseMusicViewModel : IMapFrom<SongLicense>
    {
        public string Id { get; set; }

        public LicenceViewModel License { get; set; }
    }
}
