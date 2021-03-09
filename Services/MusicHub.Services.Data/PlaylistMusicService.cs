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
    using MusicHub.Web.ViewModels.PlaylistModels;

    public class PlaylistMusicService : IPlaylistMusicService
    {
        private readonly ApplicationDbContext context;
        private readonly IPaginationService paginationService;

        public PlaylistMusicService(
            ApplicationDbContext context,
            IPaginationService paginationService)
        {
            this.context = context;
            this.paginationService = paginationService;
        }

        public PlaylistMusicsAllViewModel<T> All<T>(int page, int entitesPerPage, string id)
        {

            var playlistMusics = this.context.PlaylistMusics
              .Where(p => p.PlaylistId == id)
              .OrderByDescending(p => p.CreatedOn)
              .To<T>();

            return new PlaylistMusicsAllViewModel<T>()
            {
                PlaylistMusics = playlistMusics
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetPlaylistMusicsPagesCount(playlistMusics.Count(), entitesPerPage),
            };
        }

        public async Task Create(PlaylistMusicCreateInputModel model)
        {
            var playlistSong = model.To<PlaylistMusic>();
            await this.context.PlaylistMusics.AddAsync(playlistSong);
            await this.context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var removePlaylistMusic = this.context.PlaylistMusics.Find(id);
            this.context.PlaylistMusics.Remove(removePlaylistMusic);
            await this.context.SaveChangesAsync();
        }

        private int GetPlaylistMusicsPagesCount(int entityCount, int entitesPerPage)
            => this.paginationService.CalculatePagesCount(entityCount, entitesPerPage);
    }
}
