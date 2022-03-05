using MusicHub.Data;
using MusicHub.Services.Data.Interfaces;
using MusicHub.Services.Interfaces;
using MusicHub.Web.ViewModels.AlbumMusicModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicHub.Services.Mapping;
using MusicHub.Common.Extensions;
using MusicHub.Data.Models;

namespace MusicHub.Services.Data
{
    public class AlbumMusicService : IAlbumMusicService
    {
        private readonly ApplicationDbContext context;
        private readonly IPaginationService paginationService;

        public AlbumMusicService(
            ApplicationDbContext context,
            IPaginationService paginationService)
        {
            this.context = context;
            this.paginationService = paginationService;
        }

        public AlbumMusicsPageModel<T> All<T>(int page, int entitesPerPage, string id)
        {
            var albumMusics = this.context.AlbumMusics
              .Where(a => a.AlbumId == id)
              .OrderByDescending(p => p.Position)
              .To<T>();

            return new AlbumMusicsPageModel<T>()
            {
                AlbumMusics = albumMusics
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetDataMusicsPagesCount(albumMusics.Count(), entitesPerPage),
            };
        }

        public async Task Create(AlbumMusicCreateInputModel model)
        {
            var albumMusic = model.To<AlbumMusic>();
            var musicsInAlbum = this.context.AlbumMusics
              .Where(am => am.AlbumId == model.AlbumId);
            albumMusic.Position = musicsInAlbum.Count() == 0 ? 0 : musicsInAlbum.Max(am => am.Position) + 1;
            await this.context.AlbumMusics.AddAsync(albumMusic);
            await this.context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var removeAlbumMusic = this.context.AlbumMusics.Find(id);
            this.context.AlbumMusics.Remove(removeAlbumMusic);
            await this.context.SaveChangesAsync();
        }

        public string GetAlbumIdByAlbumMusicId(string id)
            => this.context.AlbumMusics.Where(am => am.Id == id)
                    .FirstOrDefault()
                    .AlbumId;

        public async Task Update(AlbumMusicEditInputModel model, string id)
        {
            var albumMusic = this.context.AlbumMusics.Find(id);
            var albumId = albumMusic.AlbumId;
            var oldPosition = albumMusic.Position;
            var albumMusicsToUpdatePosition = this.context.AlbumMusics
                .Where(am => am.AlbumId == albumId);

            if (oldPosition >= model.Position)
            {
                albumMusicsToUpdatePosition = albumMusicsToUpdatePosition
                    .Where(am => am.Position > model.Position && am.Position <= oldPosition);
                foreach (var albumMusicToUpdatePosition in albumMusicsToUpdatePosition)
                {
                    albumMusicToUpdatePosition.Position++;
                }
            }
            else
            {
                albumMusicsToUpdatePosition = albumMusicsToUpdatePosition
                    .Where(am => am.Position > oldPosition && am.Position <= model.Position);
                foreach (var playlistMusicToUpdatePosition in albumMusicsToUpdatePosition)
                {
                    playlistMusicToUpdatePosition.Position--;
                }
            }

            albumMusic.Position = model.Position;
            await this.context.SaveChangesAsync();
        }

        private int GetDataMusicsPagesCount(int entityCount, int entitesPerPage)
            => this.paginationService.CalculatePagesCount(entityCount, entitesPerPage);
    }
}
