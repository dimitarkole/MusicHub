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
    using MusicHub.Services.Interfaces;
    using MusicHub.Services.Mapping;
    using MusicHub.Web.ViewModels.PlayLaterMusicModels;

    public class PlayLaterMusicService : IPlayLaterMusicService
    {
        private readonly ApplicationDbContext context;
        private readonly IPaginationService paginationService;

        public PlayLaterMusicService(
            ApplicationDbContext context,
            IPaginationService paginationService)
        {
            this.context = context;
            this.paginationService = paginationService;
        }

        public PlayLaterMusicPageModel<T> All<T>(int page, int entitesPerPage, string userId)
        {
            var playLaterMusics = this.context.PlayLaterMusics
                .Where(plm => plm.UserId == userId)
                .OrderByDescending(plm => plm.Position)
                .To<T>();
            return new PlayLaterMusicPageModel<T>()
            {
                PlayLaterMusics = playLaterMusics
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetPlayedMusicPagesCount(playLaterMusics.Count(), entitesPerPage),
            };
        }

        public async Task<string> Create(PlayLaterMusicCreateInputModel model, string userId)
        {
            var playLaterMusic = model.To<PlayLaterMusic>();
            playLaterMusic.UserId = userId;
            await this.context.PlayLaterMusics.AddAsync(playLaterMusic);
            await this.context.SaveChangesAsync();
            return playLaterMusic.Id;
        }

        public async Task Delete(string id)
        {
            var playLaterMusic = this.context.PlayLaterMusics.Find(id);
            this.context.PlayLaterMusics.Remove(playLaterMusic);
            await this.context.SaveChangesAsync();
        }

        public PlayLaterMusicPageModel<T> Filter<T>(int page, int entitesPerPage, PlayLaterMusicFilterInputModels filter, string userId)
        {
            IQueryable<PlayLaterMusic> playLaterMusics = this.context.PlayLaterMusics
                 .Where(s => s.UserId == userId)
                 .OrderByDescending(plm => plm.CreatedOn);
            if (!string.IsNullOrEmpty(filter.Name))
            {
                playLaterMusics = playLaterMusics
                    .Where(s => s.UserId == userId
                        && s.Music.Name.Contains(filter.Name));
            }

            var playLaterMusicsT = playLaterMusics.To<T>();
            return new PlayLaterMusicPageModel<T>()
            {
                PlayLaterMusics = playLaterMusicsT
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetPlayedMusicPagesCount(playLaterMusicsT.Count(), entitesPerPage),
            };
        }

        public bool IsOwned(string id, string userId)
            => this.context.PlayLaterMusics.Any(pm => pm.Id == id && pm.UserId == userId);

        public async Task Update(string id, PlayLaterMusicEditInputModel model)
        {
            var playLaterMusic = this.context.PlayLaterMusics.Find(id);
            model.To<PlayLaterMusic>(playLaterMusic);
            var oldPosition = playLaterMusic.Position;
            var playLaterMusicsToUpdatePosition = this.context.PlayLaterMusics
                .Where(pm => pm.UserId == playLaterMusic.UserId);

            if (oldPosition >= model.Position)
            {
                playLaterMusicsToUpdatePosition = playLaterMusicsToUpdatePosition
                    .Where(pm => pm.Position > model.Position && pm.Position <= oldPosition);
                foreach (var playLaterMusicToUpdatePosition in playLaterMusicsToUpdatePosition)
                {
                    playLaterMusicToUpdatePosition.Position++;
                }
            }
            else
            {
                playLaterMusicsToUpdatePosition = playLaterMusicsToUpdatePosition
                    .Where(pm => pm.Position > oldPosition && pm.Position <= model.Position);
                foreach (var playLaterMusicToUpdatePosition in playLaterMusicsToUpdatePosition)
                {
                    playLaterMusicToUpdatePosition.Position--;
                }
            }

            playLaterMusic.Position = model.Position;
            await this.context.SaveChangesAsync();
        }

        private int GetPlayedMusicPagesCount(int entityCount, int entitesPerPage)
            => this.paginationService.CalculatePagesCount(entityCount, entitesPerPage);
    }
}
