namespace MusicHub.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MusicHub.Common;
    using MusicHub.Common.Extensions;
    using MusicHub.Data;
    using MusicHub.Data.Models;
    using MusicHub.Services.Interfaces;
    using MusicHub.Services.Mapping;
    using MusicHub.Web.ViewModels.LicenseModels;

    public class LicenseService : ILicenseService
    {
        private readonly ApplicationDbContext context;
        private readonly IPaginationService paginationService;

        public LicenseService(
            ApplicationDbContext context,
            IPaginationService paginationService)
        {
            this.context = context;
            this.paginationService = paginationService;
        }

        public LicenseAllViewModel<T> All<T>(int page, int entitesPerPage)
        {
            var licenses = this.context.Licenses
                .OrderByDescending(l => l.Status == LicenseStatus.WaitToBeView)
                .ThenByDescending(l => l.CreatedOn)
                .To<T>();

            return new LicenseAllViewModel<T>()
            {
                Licenses = licenses
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetLicensesPagesCount(licenses.Count(), entitesPerPage),
            };
        }

        public LicenseAllViewModel<T> AllOwn<T>(int page, int entitesPerPage, string userId)
        {
            var licenses = this.context.Licenses
                .Where(l => l.UserId == userId)
                .OrderByDescending(l => l.CreatedOn)
                .To<T>();

            return new LicenseAllViewModel<T>()
            {
                Licenses = licenses
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetLicensesPagesCount(licenses.Count(), entitesPerPage),
            };
        }

        public ICollection<T> AllOwnApproved<T>(string userId)
            => this.context.Licenses
                .Where(l => l.UserId == userId
                    && l.Status == Common.LicenseStatus.Approve)
                .OrderByDescending(l => l.Name)
                .To<T>()
                .ToList();

        public async Task ChangeStatus(string id, LicenseStatusInputModel model)
        {
            var license = this.context.Licenses.Find(id);
            license.Status = model.Status;
            this.context.Licenses.Update(license);
            await this.context.SaveChangesAsync();
        }

        public async Task<string> Create(LicenseCreateInputModel model, string userId)
        {
            var license = model.To<License>();
            license.UserId = userId;
            license.Status = Common.LicenseStatus.WaitToBeView;
            await this.context.Licenses.AddAsync(license);
            await this.context.SaveChangesAsync();

            return license.Id;
        }

        public async Task CreateFile(LicenseFileCreateInputModel model)
        {
            var licenseFile = model.To<LicenseFile>();
            await this.context.LicenseFiles.AddAsync(licenseFile);
            await this.context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var license = this.context.Licenses.Find(id);
            this.context.Licenses.Remove(license);
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteFile(string id)
        {
            var licenseFile = this.context.LicenseFiles.Find(id);
            this.context.LicenseFiles.Remove(licenseFile);
            await this.context.SaveChangesAsync();
        }

        public LicenseAllViewModel<T> Filter<T>(int page, int entitesPerPage, LicenseFilter filter, string userId = null)
        {
            IQueryable<License> licenses = this.context.Licenses;
            if (!string.IsNullOrEmpty(userId))
            {
                licenses = licenses.Where(s => s.UserId == userId);
            }

            licenses = this.FilteringLicenses(filter, licenses);

            licenses = this.OrederSearchSongs(filter.OrderMethod, licenses);
            return new LicenseAllViewModel<T>()
            {
                Licenses = licenses
                        .To<T>()
                        .GetPage(page, entitesPerPage)
                        .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetLicensesPagesCount(licenses.Count(), entitesPerPage),
            };
        }

        public T GetById<T>(string id)
            => this.context.Licenses
                .Where(s => s.Id == id)
                .To<T>()
                .FirstOrDefault();

        public bool IsOwn(string licenseId, string userId)
            => this.context.Licenses
                .Any(s =>
                    s.Id == licenseId
                    && s.UserId == userId);

        public async Task Update(string id, LicenseEditInputModel model)
        {
            var license = this.context.Licenses.Find(id);
            license.Name = model.Name;
            license.Status = Common.LicenseStatus.WaitToBeView;
            this.context.Licenses.Update(license);
            await this.context.SaveChangesAsync();
        }

        private IQueryable<License> FilteringLicenses(LicenseFilter filter, IQueryable<License> licenses)
        {
            if (!string.IsNullOrEmpty(filter.UserId))
            {
                licenses = licenses.Where(s => s.UserId == filter.UserId);
            }

            if (!string.IsNullOrEmpty(filter.Name))
            {
                licenses = licenses.Where(s => s.Name.ToLower().Contains(filter.Name.ToLower()));
            }

            if (!string.IsNullOrEmpty(filter.Username))
            {
                licenses = licenses.Where(s => s.User.UserName.ToLower().Contains(filter.Username.ToLower()));
            }

            return licenses;
        }

        private IQueryable<License> OrederSearchSongs(OrderMethod orderMethod, IQueryable<License> licenses)
        {
            if (orderMethod == OrderMethod.CreatedOnAsc)
            {
                licenses = licenses.OrderBy(s => s.CreatedOn);
            }
            else if (orderMethod == OrderMethod.CreatedOnDesc)
            {
                licenses = licenses.OrderByDescending(s => s.CreatedOn);
            }
            else if (orderMethod == OrderMethod.NameAsc)
            {
                licenses = licenses.OrderBy(s => s.Name);
            }
            else if (orderMethod == OrderMethod.NameDesc)
            {
                licenses = licenses.OrderByDescending(s => s.Name);
            }

            return licenses;
        }

        private int GetLicensesPagesCount(int entityCount, int entitesPerPage)
             => this.paginationService.CalculatePagesCount(entityCount, entitesPerPage);
    }
}
