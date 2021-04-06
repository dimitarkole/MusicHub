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
    using MusicHub.Services.Data.Interfaces;
    using MusicHub.Services.Mapping;
    using MusicHub.Web.ViewModels.PlayedMusicModels;
    using MusicHub.Services.Interfaces;

    public class PlayedMusicService : IPlayedMusicService
    {
        private readonly ApplicationDbContext context;
        private readonly IPaginationService paginationService;

        public PlayedMusicService(
            ApplicationDbContext context,
            IPaginationService paginationService)
        {
            this.context = context;
            this.paginationService = paginationService;
        }

        public PlayedMusicAllViewModel<T> All<T>(int page, int entitesPerPage, string userId)
        {
            var playedMusic = this.context.PlayedMusics
                .Where(s => s.UserId == userId)
                .OrderByDescending(s => s.CreatedOn)
                .To<T>();

            return new PlayedMusicAllViewModel<T>()
            {
                PlayedMusic = playedMusic
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetPlayedMusicPagesCount(playedMusic.Count(), entitesPerPage),
            };
        }

        public async Task Create(PlayedMusicCreateInputModel model, string userId)
        {
            var playedMusic = model.To<PlayedMusic>();
            playedMusic.UserId = userId;
            await this.context.PlayedMusics.AddAsync(playedMusic);
            await this.context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var playedMusic = this.context.PlayedMusics.Find(id);
            this.context.PlayedMusics.Remove(playedMusic);
            await this.context.SaveChangesAsync();
        }

        public PlayedMusicAllViewModel<T> Filter<T>(int page, int entitesPerPage, PlayedMusicFilterInputModels filter, string userId)
        {

            IQueryable<PlayedMusic> playedMusic = this.context.PlayedMusics
                 .Where(s => s.UserId == userId)
                 .OrderByDescending(s => s.CreatedOn);
            if (!string.IsNullOrEmpty(filter.Name))
            {
                playedMusic = playedMusic
                    .Where(s => s.UserId == userId
                        && s.Music.Name.Contains(filter.Name));
            }

            var playedMusicT = playedMusic.To<T>();
            return new PlayedMusicAllViewModel<T>()
            {
                PlayedMusic = playedMusicT
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetPlayedMusicPagesCount(playedMusicT.Count(), entitesPerPage),
            };
        }

        public bool IsOwned(string id, string userId)
            => this.context.PlayedMusics.Any(pm => pm.Id == id && pm.UserId == userId);

        private int GetPlayedMusicPagesCount(int entityCount, int entitesPerPage)
            => this.paginationService.CalculatePagesCount(entityCount, entitesPerPage);
    }
}
