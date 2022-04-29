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
    using MusicHub.Data.Models.Enums;
    using MusicHub.Services.Data.Interfaces;
    using MusicHub.Services.Interfaces;
    using MusicHub.Services.Mapping;
    using MusicHub.Web.ViewModels.SubscriptionClaimModels;
    using MusicHub.Web.ViewModels.SubscriptionModels;

    public class SubscriptionService : ISubscriptionService
    {
        private readonly ApplicationDbContext context;
        private readonly IPaginationService paginationService;

        public SubscriptionService(
            ApplicationDbContext context,
            IPaginationService paginationService)
        {
            this.context = context;
            this.paginationService = paginationService;
        }

        public SubscriptionPageModel<T> All<T>(int page, int entitesPerPage)
        {
            var subscriptions = this.context.Subscriptions
                    .OrderBy(s => s.Name)
                    .To<T>();
            return new SubscriptionPageModel<T>()
            {
                Subscriptions = subscriptions
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetDataPagesCount(subscriptions.Count(), entitesPerPage),
            };
        }

        public async Task<string> Create(SubscriptionCreateInputModel model)
        {
            var subscription = model.To<Subscription>();
            await this.context.Subscriptions.AddAsync(subscription);
            await this.context.SaveChangesAsync();
            this.AddSubcriprionClaim(subscription.Id, model.SubcriptionClaims);
            await this.context.SaveChangesAsync();
            return subscription.Id;
        }

        public async Task Delete(string id)
        {
            var subscription = this.context.Subscriptions.Find(id);
            this.context.Subscriptions.Remove(subscription);
            this.RemoveOldSubcriprionClaim(id);
            await this.context.SaveChangesAsync();
        }

        public SubscriptionPageModel<T> Filter<T>(int page, int entitesPerPage, SubscriptionFilter filter)
        {
            IQueryable<Subscription> subscriptions = this.context.Subscriptions
                     .OrderBy(s => s.Name);
            if (!string.IsNullOrEmpty(filter.Name))
            {
              subscriptions = subscriptions.Where(s => s.Name.Contains(filter.Name));
            }

            if (filter.Type != SubscriptionType.All)
            {
                subscriptions = subscriptions.Where(s => s.Type == filter.Type);
            }

            subscriptions = this.OrederSearchSubscription(filter.OrderMethod, subscriptions);
            return new SubscriptionPageModel<T>()
            {
                Subscriptions = subscriptions
                    .To<T>()
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetDataPagesCount(subscriptions.Count(), entitesPerPage),
            };
        }

        public T GetById<T>(string id)
            => this.context.Subscriptions
                .Where(s => s.Id == id)
                .To<T>()
                .FirstOrDefault();

        public async Task Update(string id, SubscriptionEditInputModel model)
        {
            var subscription = this.context.Subscriptions.Find(id);
            model.To<Subscription>(subscription);
            this.RemoveOldSubcriprionClaim(id);
            await this.context.SaveChangesAsync();
            this.AddSubcriprionClaim(subscription.Id, model.SubcriptionClaims);
            this.context.Subscriptions.Update(subscription);
            await this.context.SaveChangesAsync();
        }

        private void RemoveOldSubcriprionClaim(string subscriptionId)
        {
            var oldSubscriptionClaims = this.context.SubscriptionClaims
                .Where(sc => sc.SubcriptionId == subscriptionId);
            this.context.SubscriptionClaims.RemoveRange(oldSubscriptionClaims);
        }

        private void AddSubcriprionClaim(string subscriptionId, ICollection<SubscriptionClaimCreateInputModel> models)
        {
            var subscriptionClaims = models.To<SubscriptionClaim>();
            foreach (var subscriptionClaim in subscriptionClaims)
            {
                subscriptionClaim.SubcriptionId = subscriptionId;
            }

            this.context.SubscriptionClaims.AddRange(subscriptionClaims);
        }

        private IQueryable<Subscription> OrederSearchSubscription(OrderMethod orderMethod, IQueryable<Subscription> subscriptions)
        {
             if (orderMethod == OrderMethod.CreatedOnAsc)
             {
                subscriptions = subscriptions.OrderBy(s => s.CreatedOn);
             }
             else if (orderMethod == OrderMethod.CreatedOnDesc)
             {
                subscriptions = subscriptions.OrderByDescending(s => s.CreatedOn);
             }
             else if (orderMethod == OrderMethod.NameAsc)
             {
                subscriptions = subscriptions.OrderBy(s => s.Name);
             }
             else if (orderMethod == OrderMethod.NameDesc)
             {
                subscriptions = subscriptions.OrderByDescending(s => s.Name);
             }
             else if (orderMethod == OrderMethod.PriceAsc)
             {
                subscriptions = subscriptions.OrderBy(s => s.Price);
             }
             else if (orderMethod == OrderMethod.PriceDesc)
             {
                subscriptions = subscriptions.OrderByDescending(s => s.Price);
             }

             return subscriptions;
        }

        private int GetDataPagesCount(int entityCount, int entitesPerPage)
            => this.paginationService.CalculatePagesCount(entityCount, entitesPerPage);
    }
}
