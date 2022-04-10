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
    using MusicHub.Web.ViewModels.PaySubscriptionModels;

    public class PaySubscriptionService : IPaySubscriptionService
    {
        private readonly ApplicationDbContext context;
        private readonly IPaginationService paginationService;

        public PaySubscriptionService(
            ApplicationDbContext context,
            IPaginationService paginationService)
        {
            this.context = context;
            this.paginationService = paginationService;
        }

        public PaySubscriptionPageModel<T> All<T>(int page, int entitesPerPage, string userId = null)
        {
            IQueryable<PaySubscription> paySubscriptions = this.context.PaySubscriptions;
            if (!string.IsNullOrEmpty(userId))
            {
                paySubscriptions = paySubscriptions.Where(s => s.UserId == userId);
            }

            var paySubscriptionsT = paySubscriptions.To<T>();
            return new PaySubscriptionPageModel<T>()
            {
                PaySubscriptions = paySubscriptionsT
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetDataPagesCount(paySubscriptionsT.Count(), entitesPerPage),
            };
        }

        public async Task<string> Create(PaySubscriptionCreateInputModel model, string userId)
        {
            var paySubcription = model.To<PaySubscription>();
            paySubcription.UserId = userId;
            await this.context.PaySubscriptions.AddAsync(paySubcription);
            await this.context.SaveChangesAsync();
            return paySubcription.Id;
        }

        public PaySubscriptionPageModel<T> Filter<T>(int page, int entitesPerPage, PaySubscriptionFilter filter, string userId = null)
        {
            IQueryable<PaySubscription> paySubscriptions = this.context.PaySubscriptions;
            if (!string.IsNullOrEmpty(userId))
            {
                paySubscriptions = paySubscriptions.Where(s => s.UserId == userId);
            }

            paySubscriptions = this.Filtering(filter, paySubscriptions);
            paySubscriptions = this.Ordering(filter.OrderMethod, paySubscriptions);
            var paySubscriptionsT = paySubscriptions.To<T>();
            return new PaySubscriptionPageModel<T>()
            {
                PaySubscriptions = paySubscriptionsT
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetDataPagesCount(paySubscriptionsT.Count(), entitesPerPage),
            };
        }

        public T GetById<T>(string id)
            => this.context.PaySubscriptions
                .Where(a => a.Id == id)
                .To<T>()
                .FirstOrDefault();

        public bool IsOwned(string id, string userId)
            => this.context.PaySubscriptions
            .Any(a => a.Id == id && a.UserId == userId);

        public async Task Update(string id, PaySubscriptionEditModel model)
        {
            var paySubscripton = this.context.PaySubscriptions
                .FirstOrDefault(a => a.Id == id);
            model.To<Album>(paySubscripton);
            this.context.PaySubscriptions.Update(paySubscripton);
            await this.context.SaveChangesAsync();
        }

        private IQueryable<PaySubscription> Filtering(PaySubscriptionFilter filter, IQueryable<PaySubscription> paySubscriptions)
        {
            if (filter.ValidFrom != null)
            {
                paySubscriptions = paySubscriptions.Where(ps => ps.ValidFrom >= filter.ValidFrom);
            }

            if (filter.ValidFrom != null)
            {
                paySubscriptions = paySubscriptions.Where(ps => ps.ValidTo <= filter.ValidTo);
            }

            return paySubscriptions;
        }

        private IQueryable<PaySubscription> Ordering(OrderMethod orderMethod, IQueryable<PaySubscription> paySubscriptions)
        {
            if (orderMethod == OrderMethod.DateDesc)
            {
                return paySubscriptions.OrderByDescending(ps => ps.ValidFrom);
            }

            return paySubscriptions.OrderBy(ps => ps.ValidFrom);
        }

        private int GetDataPagesCount(int entityCount, int entitesPerPage)
            => this.paginationService.CalculatePagesCount(entityCount, entitesPerPage);
    }
}
