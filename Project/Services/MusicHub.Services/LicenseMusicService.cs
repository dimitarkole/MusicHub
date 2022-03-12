namespace MusicHub.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Data;
    using MusicHub.Data.Models;
    using MusicHub.Services.Interfaces;
    using MusicHub.Services.Mapping;
    using MusicHub.Web.ViewModels.LicenseMusicModels;

    public class LicenseMusicService : ILicenseMusicService
    {
        private readonly ApplicationDbContext context;

        public LicenseMusicService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<T> All<T>(string songId)
            => this.context.SongLicenses
                .Where(x => x.SongId == songId)
                .To<T>()
                .ToList();

        public async Task Create(LicenseMusicCreateModel model)
        {
            var licenseMusic = model.To<SongLicense>();
            await this.context.SongLicenses.AddAsync(licenseMusic);
            await this.context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var licenseMusic = this.context.SongLicenses.Find(id);
            this.context.SongLicenses.Remove(licenseMusic);
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteAllMusicLicenses(string musicId)
        {
            var licenseMusic = this.context.SongLicenses.Where(sl => sl.SongId == musicId);
            this.context.SongLicenses.RemoveRange(licenseMusic);
            await this.context.SaveChangesAsync();
        }
    }
}
