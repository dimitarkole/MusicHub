namespace MusicHub.Services.Data.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Web.ViewModels.LicenseMusicModels;

    public interface ILicenseMusicService
    {
        List<T> All<T>(string musicId);

        Task<string> Create(LicenseMusicCreateModel model);

        Task Delete(string id);

        Task DeleteAllMusicLicenses(string musicId);
    }
}
