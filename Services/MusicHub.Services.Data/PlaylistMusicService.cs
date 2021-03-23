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
              .OrderByDescending(p => p.Position)
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
            var playlistMusic = model.To<PlaylistMusic>();
            var musicsInPlaylist = this.context.PlaylistMusics
              .Where(pm => pm.PlaylistId == model.PlaylistId);
            playlistMusic.Position = musicsInPlaylist.Count() == 0 ? 0 : musicsInPlaylist.Max(pm => pm.Position);
            await this.context.PlaylistMusics.AddAsync(playlistMusic);
            await this.context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var removePlaylistMusic = this.context.PlaylistMusics.Find(id);
            this.context.PlaylistMusics.Remove(removePlaylistMusic);
            await this.context.SaveChangesAsync();
        }

        public async Task Update(PlaylistMusicEditInputModel model, string id)
        {
            var playlistMusic = this.context.PlaylistMusics.Find(id);
            var playlistId = playlistMusic.PlaylistId;
            var oldPosition = playlistMusic.Position;
            var playlistMusicsToUpdatePosition = this.context.PlaylistMusics
                .Where(pm => pm.PlaylistId == playlistId);

            if(oldPosition >= model.Position)
            {
                playlistMusicsToUpdatePosition = playlistMusicsToUpdatePosition
                    .Where(pm => pm.Position > model.Position && pm.Position <= oldPosition);
                foreach (var playlistMusicToUpdatePosition in playlistMusicsToUpdatePosition)
                {
                    playlistMusicToUpdatePosition.Position++;
                }
            }
            else
            {
                playlistMusicsToUpdatePosition = playlistMusicsToUpdatePosition
                    .Where(pm => pm.Position > oldPosition && pm.Position <= model.Position);
                foreach (var playlistMusicToUpdatePosition in playlistMusicsToUpdatePosition)
                {
                    playlistMusicToUpdatePosition.Position--;
                }
            }

            playlistMusic.Position = model.Position;
            await this.context.SaveChangesAsync();
        }

        private int GetPlaylistMusicsPagesCount(int entityCount, int entitesPerPage)
            => this.paginationService.CalculatePagesCount(entityCount, entitesPerPage);
    }
}
