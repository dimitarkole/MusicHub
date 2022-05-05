namespace MusicHub.Services.Data
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
    using MusicHub.Services.Data.Interfaces;
    using MusicHub.Services.Interfaces;
    using MusicHub.Services.Mapping;
    using MusicHub.Web.ViewModels.BellModels;

    public class BellService : IBellService
    {
        private readonly ApplicationDbContext context;
        private readonly IPaginationService paginationService;

        public BellService(ApplicationDbContext context,
            IPaginationService paginationService)
        {
            this.context = context;
            this.paginationService = paginationService;
        }

        public BellerPageModel<T> AllBellers<T>(int page, int entitesPerPage, string userId)
        {
            var bellers = this.context.Bells
                .Where(b => b.BellingId == userId)
                .To<T>();

            return new BellerPageModel<T>()
            {
                Bellers = bellers
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetDataPagesCount(bellers.Count(), entitesPerPage),
            };
        }

        public BellingPageModel<T> AllBellings<T>(int page, int entitesPerPage, string userId)
        {
            var bellings = this.context.Bells
                .Where(b => b.BellerId == userId)
                .To<T>()
                .ToList();
            return new BellingPageModel<T>()
            {
                Bellings = bellings
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetDataPagesCount(bellings.Count(), entitesPerPage),
            };
        }

        public async Task Create(BellCreateInputModel model, string userId)
        {
            var beel = model.To<Bell>();
            beel.BellerId = userId;
            await this.context.Bells.AddAsync(beel);
            await this.context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var bells = this.context.Bells
                .Where(b => b.Id == id)
                .FirstOrDefault();
            this.context.Bells.Remove(bells);
            await this.context.SaveChangesAsync();
        }

        public BellerPageModel<T> FilterBellers<T>(int page, int entitesPerPage, BellFilterInputModel filter, string userId)
        {
            var bellers = this.context.Bells
              .Where(b => b.BellingId == userId);
            bellers = this.FilteringBellers(filter, bellers);
            bellers = this.OrederBellers(filter.OrderMethod, bellers);
            var bellersT = bellers.To<T>();
            return new BellerPageModel<T>()
            {
                Bellers = bellersT
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetDataPagesCount(bellersT.Count(), entitesPerPage),
            };
        }

        public BellingPageModel<T> FilterBellings<T>(int page, int entitesPerPage, BellFilterInputModel filter, string userId)
        {
            var bellings = this.context.Bells
                .Where(b => b.BellerId == userId);
            bellings = this.FilteringBellings(filter, bellings);
            bellings = this.OrederBellings(filter.OrderMethod, bellings);
            var bellingsT = bellings.To<T>();
            return new BellingPageModel<T>()
            {
                Bellings = bellingsT
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetDataPagesCount(bellingsT.Count(), entitesPerPage),
            };
        }

        public T GetBell<T>(string bellerUserId, string bellingUserId)
           => this.context.Bells
                    .Where(b => b.BellerId == bellerUserId
                        && b.BellingId == bellingUserId)
                    .To<T>()
                    .FirstOrDefault();

        public bool IsBelling(string bellerUserId, string bellingUserId)
            => this.context.Bells
             .Any(b => b.BellerId == bellerUserId
                 && b.BellingId == bellingUserId);

        public bool IsOwned(string id, string userId)
            => this.context.Bells
                .Any(b => b.Id == id
                    && b.BellerId == userId);

        private IQueryable<Bell> FilteringBellings(BellFilterInputModel filter, IQueryable<Bell> bells)
        {
            if (!string.IsNullOrEmpty(filter.FirstName))
            {
                bells = bells.Where(b => b.Belling.FirstName.ToLower().Contains(filter.FirstName.ToLower()));
            }

            if (!string.IsNullOrEmpty(filter.LastName))
            {
                bells = bells.Where(b => b.Belling.LastName.ToLower().Contains(filter.LastName.ToLower()));
            }

            if (!string.IsNullOrEmpty(filter.Username))
            {
                bells = bells.Where(b => b.Belling.UserName.ToLower().Contains(filter.Username.ToLower()));
            }

            return bells;
        }

        private IQueryable<Bell> FilteringBellers(BellFilterInputModel filter, IQueryable<Bell> bells)
        {
            if (!string.IsNullOrEmpty(filter.FirstName))
            {
                bells = bells.Where(b => b.Beller.FirstName.ToLower().Contains(filter.FirstName.ToLower()));
            }

            if (!string.IsNullOrEmpty(filter.LastName))
            {
                bells = bells.Where(b => b.Beller.LastName.ToLower().Contains(filter.LastName.ToLower()));
            }

            if (!string.IsNullOrEmpty(filter.Username))
            {
                bells = bells.Where(b => b.Beller.UserName.ToLower().Contains(filter.Username.ToLower()));
            }

            return bells;
        }

        private IQueryable<Bell> OrederBellers(OrderMethod orderMethod, IQueryable<Bell> bells)
        {
            if (orderMethod == OrderMethod.NameAsc)
            {
                bells = bells.OrderBy(b => b.Beller.UserName);
            }
            else if (orderMethod == OrderMethod.NameDesc)
            {
                bells = bells.OrderByDescending(b => b.Beller.UserName);
            }
            else if (orderMethod == OrderMethod.CreatedOnAsc)
            {
                bells = bells.OrderBy(b => b.CreatedOn);
            }
            else if (orderMethod == OrderMethod.CreatedOnDesc)
            {
                bells = bells.OrderByDescending(b => b.CreatedOn);
            }

            return bells;
        }

        private IQueryable<Bell> OrederBellings(OrderMethod orderMethod, IQueryable<Bell> bells)
        {
            if (orderMethod == OrderMethod.NameDesc)
            {
                bells = bells.OrderByDescending(b => b.Belling.UserName);
            }
            else if (orderMethod == OrderMethod.NameAsc)
            {
                bells = bells.OrderBy(b => b.Belling.UserName);
            }
            else if (orderMethod == OrderMethod.CreatedOnAsc)
            {
                bells = bells.OrderBy(b => b.CreatedOn);
            }
            else if (orderMethod == OrderMethod.CreatedOnDesc)
            {
                bells = bells.OrderByDescending(b => b.CreatedOn);
            }

            return bells;
        }

        private int GetDataPagesCount(int entityCount, int entitesPerPage)
            => this.paginationService.CalculatePagesCount(entityCount, entitesPerPage);
    }
}
