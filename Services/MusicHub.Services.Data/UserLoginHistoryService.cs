namespace MusicHub.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Common.Extensions;
    using MusicHub.Data;
    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.Services.Data.Interfaces;
    using MusicHub.Services.Interfaces;
    using MusicHub.Services.Mapping;
    using MusicHub.Services.Messaging;
    using MusicHub.Services.Messaging.Infrastucture;
    using MusicHub.Web.ViewModels.LicenseModels;
    using MusicHub.Web.ViewModels.UserLoginHistoryModels;
    using SendGrid.Helpers.Mail.Model;
    using MusicHub.Common.Extensions;

    using static MusicHub.Common.GlobalConstants;

    public class UserLoginHistoryService : IUserLoginHistoryService
    {
        private readonly ApplicationDbContext context;
        private readonly IEmailSender emailSender;
        private readonly IPaginationService paginationService;

        public UserLoginHistoryService(
            ApplicationDbContext dbContext,
            IPaginationService paginationService,
            IEmailSender emailSender)
        {
            this.context = dbContext;
            this.emailSender = emailSender;
            this.paginationService = paginationService;
        }

        public async Task<string> Create(string userId, UserLoginHistoryCreateInputModel model)
        {
            var history = model.To<UserLoginHistory>();
            history.UserId = userId;
            await this.context.UserLoginHistories.AddAsync(history);
            await this.context.SaveChangesAsync();

            return history.Id;
        }

        public async Task Delete(string id)
        {
            var userLoginHistory = this.context.UserLoginHistories.Find(id);
            this.context.UserLoginHistories.Remove(userLoginHistory);
            await this.context.SaveChangesAsync();
        }

        public UserLoginHistoryPageModel<T> Filter<T>(int page, int entitesPerPage, string userId, UserLoginHistoryFilterInputModel filter)
        {
            IQueryable<UserLoginHistory> userLoginHistory = this.context.UserLoginHistories;
            if (!string.IsNullOrEmpty(userId))
            {
                userLoginHistory = userLoginHistory.Where(s => s.UserId == userId);
            }

            userLoginHistory = this.Filtering(filter, userLoginHistory);
            userLoginHistory = userLoginHistory.OrderByDescending(l => l.LoginDate);

            return new UserLoginHistoryPageModel<T>()
            {
                UserLoginHistory = userLoginHistory
                    .To<T>()
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetUserLoginHistoryPagesCount(userLoginHistory.Count(), entitesPerPage),
            };
        }

        public T GetById<T>(string id)
            => this.context.UserLoginHistories
                .Where(s => s.Id == id)
                .To<T>()
                .FirstOrDefault();

        public async Task HandleLoginAsync(User user, string ipAddress, string userAgent)
        {
            if (string.IsNullOrWhiteSpace(ipAddress))
            {
                return;
            }

            var isKnownIp = this.IsKnownIpAsync(user.Id, ipAddress);

            if (!isKnownIp)
            {
                await this.SendNewIpEmailAsync(user, ipAddress);
            }

            await this.Create(user.Id, ipAddress, userAgent);
        }

        private int GetUserLoginHistoryPagesCount(int entityCount, int entitesPerPage)
            => this.paginationService.CalculatePagesCount(entityCount, entitesPerPage);

        private IQueryable<UserLoginHistory> Filtering(UserLoginHistoryFilterInputModel filter, IQueryable<UserLoginHistory> userLoginHistory)
        {
            if (filter.LoginDateFrom != null && filter.LoginDateFrom != default(DateTime))
            {
                userLoginHistory = userLoginHistory.Where(h => h.LoginDate >= filter.LoginDateFrom);
            }

            if (filter.LoginDateTo != null && filter.LoginDateTo != default(DateTime))
            {
                userLoginHistory = userLoginHistory.Where(h => h.LoginDate <= filter.LoginDateTo);
            }

            return userLoginHistory;
        }

        private bool IsKnownIpAsync(string userId, string ipAddress)
        {
            return this.context.UserLoginHistories
                .Any(x => x.UserId == userId && x.IpAddress == ipAddress);
        }

        private async Task SendNewIpEmailAsync(User user, string ipAddress)
        {
            var data = new Dictionary<string, string>
            {
                { "userName", user.UserName },
                { "ipAddress", ipAddress }, // FIX
                { "time", DateTime.UtcNow.ToString("dd.MM.yyyy HH:mm:ss") }
            };

            var htmlContent = MessagesContentTemplates
                .CreateMessageContent(EmailSubjects.NewIPLoginDetected, data);

            await this.emailSender.SendEmailAsync(
                user.Email,
                EmailSubjects.NewIPLoginDetected,
                htmlContent);
        }

        public async Task<string> Create(string userId, string ipAddress, string userAgent)
        {
            var history = new UserLoginHistory
            {
                Id = Guid.NewGuid().ToString(),
                UserId = userId,
                IpAddress = ipAddress,
                UserAgent = userAgent,
                LoginDate = DateTime.UtcNow
            };

            await this.context.UserLoginHistories.AddAsync(history);
            await this.context.SaveChangesAsync();

            return history.Id;
        }
    }
}
