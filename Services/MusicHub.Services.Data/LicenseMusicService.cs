namespace MusicHub.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Data;
    using MusicHub.Data.Models;
    using MusicHub.Services.Data.Interfaces;
    using MusicHub.Services.Mapping;
    using MusicHub.Web.ViewModels.LicenseMusicModels;

    public class LicenseMusicService : ILicenseMusicService
    {
        private readonly ApplicationDbContext context;

        public LicenseMusicService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<T> All<T>(string musicId)
            => this.context.MusicLicenses
                .Where(x => x.MusicId == musicId)
                .To<T>()
                .ToList();

        public async Task Create(LicenseMusicCreateModel model)
        {
            var licenseMusic = model.To<MusicLicense>();
            await this.context.MusicLicenses.AddAsync(licenseMusic);
            await this.context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var licenseMusic = this.context.MusicLicenses.Find(id);
            this.context.MusicLicenses.Remove(licenseMusic);
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteAllMusicLicenses(string musicId)
        {
            var licenseMusic = this.context.MusicLicenses.Where(sl => sl.MusicId == musicId);
            this.context.MusicLicenses.RemoveRange(licenseMusic);
            await this.context.SaveChangesAsync();
        }
    }
}
