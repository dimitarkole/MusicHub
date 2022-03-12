namespace MusicHub.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MusicHub.Common.Extensions;
    using MusicHub.Data;
    using MusicHub.Data.Models;
    using MusicHub.Services.Interfaces;
    using MusicHub.Services.Mapping;
    using MusicHub.Web.ViewModels.SongViewHistoryModels;

    public class SongViewHistoryService : ISongViewHistoryService
    {
        private readonly ApplicationDbContext context;
        private readonly IPaginationService paginationService;

        public SongViewHistoryService(
            ApplicationDbContext context,
            IPaginationService paginationService)
        {
            this.context = context;
            this.paginationService = paginationService;
        }

        public SongViewHistoryAllViewModel<T> All<T>(int page, int entitesPerPage, string userId)
        {
            var songsViewHistory = this.context.SongViewHistories
                .Where(s => s.UserId == userId)
                .OrderByDescending(s => s.CreatedOn)
                .To<T>();

            return new SongViewHistoryAllViewModel<T>()
            {
                SongViewHistory = songsViewHistory
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetSongsViewHistoryPagesCount(songsViewHistory.Count(), entitesPerPage),
            };
        }

        public SongViewHistoryAllViewModel<T> Search<T>(int page, int entitesPerPage, SongViewHistoryFilterInputModels filter, string userId)
        {
            IQueryable<SongViewHistory> songViewHistories = this.context.SongViewHistories
                 .Where(s => s.UserId == userId);
            if (!string.IsNullOrEmpty(filter.Name))
            {
                songViewHistories = songViewHistories
                    .Where(s => s.UserId == userId
                        && s.Song.Name.Contains(filter.Name));
            }

            var songViewHistoriesT = songViewHistories.To<T>();
            return new SongViewHistoryAllViewModel<T>()
            {
                SongViewHistory = songViewHistoriesT
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetSongsViewHistoryPagesCount(songViewHistoriesT.Count(), entitesPerPage),
            };
        }

        public async Task Create(SongViewHistoryCreateInputModel model, string userId)
        {
            SongViewHistory songViewHistory = new SongViewHistory
            {
                UserId = userId,
                SongId = model.SongId,
            };
            await this.context.SongViewHistories.AddAsync(songViewHistory);
            await this.context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            SongViewHistory songViewHistory = this.context.SongViewHistories.Find(id);
            this.context.SongViewHistories.Remove(songViewHistory);
            await this.context.SaveChangesAsync();
        }

        private int GetSongsViewHistoryPagesCount(int entityCount, int entitesPerPage)
            => this.paginationService.CalculatePagesCount(entityCount, entitesPerPage);
    }
}
