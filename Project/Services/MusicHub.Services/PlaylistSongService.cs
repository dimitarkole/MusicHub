namespace MusicHub.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Common.Extensions;
    using MusicHub.Data;
    using MusicHub.Data.Models;
    using MusicHub.Services.Interfaces;
    using MusicHub.Services.Mapping;
    using MusicHub.Web.ViewModels.PlaylistModels;

    public class PlaylistSongService : IPlaylistSongService
    {
        private readonly ApplicationDbContext context;
        private readonly IPaginationService paginationService;

        public PlaylistSongService(
            ApplicationDbContext context,
            IPaginationService paginationService)
        {
            this.context = context;
            this.paginationService = paginationService;
        }

        public async Task Create(SongToPlaylistCreateInputModel model)
        {
            PlaylistSong playlistSong = model.To<PlaylistSong>();
            await this.context.PlaylistSongs.AddAsync(playlistSong);
            await this.context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var removePlaylistSong = this.context.PlaylistSongs.Find(id);
            this.context.PlaylistSongs.Remove(removePlaylistSong);
            await this.context.SaveChangesAsync();
        }

        public PlaylistSongsAllViewModel<T> All<T>(int page, int entitesPerPage, string id)
        {
            var playlistSongs = this.context.PlaylistSongs
              .Where(p => p.PlaylistId == id)
              .OrderByDescending(p => p.CreatedOn)
              .To<T>();

            return new PlaylistSongsAllViewModel<T>()
            {
                PlaylistSongs = playlistSongs
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetPlaylistSongsPagesCount(playlistSongs.Count(), entitesPerPage),
            };
        }

        private int GetPlaylistSongsPagesCount(int entityCount, int entitesPerPage)
           => this.paginationService.CalculatePagesCount(entityCount, entitesPerPage);
    }
}
