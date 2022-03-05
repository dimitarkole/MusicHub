namespace MusicHub.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MusicHub.Data;
    using MusicHub.Data.Models.Enums;
    using MusicHub.Services.Data.Interfaces;
    using MusicHub.Common.Extensions;
    using MusicHub.Services.Interfaces;
    using MusicHub.Web.ViewModels.AlbumModels;
    using MusicHub.Services.Mapping;
    using MusicHub.Common;
    using MusicHub.Data.Models;

    public class AlbumService : IAlbumService
    {
        private readonly ApplicationDbContext context;
        private readonly IPaginationService paginationService;

        public AlbumService(
            ApplicationDbContext context,
            IPaginationService paginationService)
        {
            this.context = context;
            this.paginationService = paginationService;
        }

        public AlbumPageModel<T> All<T>(int page, int entitesPerPage, string userId = null)
        {
            var albums = this.context.Albums
              .Where(a => a.AlbumMusics.Count > 0);
            if (!string.IsNullOrEmpty(userId))
            {
                albums = albums.Where(s => s.UserId == userId);
            }
            else
            {
                albums = albums.Where(m => m.Privacy == Privacy.Public);
            }

            var albumsT = albums.OrderByDescending(p => p.CreatedOn)
              .To<T>();
            return new AlbumPageModel<T>()
            {
                Albums = albumsT
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetDataPagesCount(albums.Count(), entitesPerPage),
            };
        }

        public async Task<string> Create(AlbumCreateInputModel model, string userId)
        {
            var album = model.To<Album>();
            album.UserId = userId;
            await this.context.Albums.AddAsync(album);
            await this.context.SaveChangesAsync();
            return album.Id;
        }

        public async Task Delete(string id)
        {
            var album = this.context.Albums.Find(id);
            this.context.Albums.Remove(album);
            await this.context.SaveChangesAsync();
        }

        public AlbumPageModel<T> Filter<T>(int page, int entitesPerPage, AlbumFilterInputModel model, string userId = null)
        {
            var albums = this.context.Albums
               .Where(a => a.AlbumMusics.Count > 0);

            if (!string.IsNullOrEmpty(userId))
            {
                albums = albums.Where(s => s.UserId == userId);
            }
            else
            {
                albums = albums.Where(m => m.Privacy == Privacy.Public);
            }

            albums = this.FilteringAlbum(model, albums);
            albums = this.OrderSearchAlbums(model.OrderMethod, albums);
            var albumsT = albums.To<T>();
            return new AlbumPageModel<T>()
            {
                Albums = albumsT
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetDataPagesCount(albumsT.Count(), entitesPerPage),
            };
        }

        public T GetById<T>(string id)
            => this.context.Albums
                .Where(a => a.Id == id)
                .To<T>()
                .FirstOrDefault();

        public IList<T> GetOwnedForAddingMusic<T>(string musicId, string userId)
            => this.context.Albums
                .Where(a => a.UserId == userId
                     && a.AlbumMusics.Any(ps => ps.MusicId == musicId) == false)
                .OrderByDescending(ps => ps.Name)
                .To<T>()
                .ToList();

        public bool IsOwned(string id, string userId)
            => this.context.Albums
               .Any(a => a.Id == id && a.UserId == userId);

        public async Task Update(AlbumEditInputModel model, string id)
        {
            var album = this.context.Albums
                 .FirstOrDefault(a => a.Id == id);
            model.To<Album>(album);
            this.context.Albums.Update(album);
            await this.context.SaveChangesAsync();
        }

        private IQueryable<Album> FilteringAlbum(AlbumFilterInputModel filter, IQueryable<Album> albums)
        {
            if (!string.IsNullOrEmpty(filter.UserId))
            {
                albums = albums.Where(s => s.UserId == filter.UserId);
            }
            else if (!string.IsNullOrEmpty(filter.Name))
            {
                albums = albums.Where(p => p.Name.ToLower().Contains(filter.Name.ToLower()));
            }

            return albums;
        }

        private IQueryable<Album> OrderSearchAlbums(OrderMethod orderMethod, IQueryable<Album> albums)
        {
            if (orderMethod == OrderMethod.CreatedOnAsc)
            {
                albums = albums.OrderBy(s => s.CreatedOn);
            }
            else if (orderMethod == OrderMethod.CreatedOnDesc)
            {
                albums = albums.OrderByDescending(s => s.CreatedOn);
            }
            else if (orderMethod == OrderMethod.NameAsc)
            {
                albums = albums.OrderBy(s => s.Name);
            }
            else if (orderMethod == OrderMethod.NameDesc)
            {
                albums = albums.OrderByDescending(s => s.Name);
            }

            return albums;
        }

        private int GetDataPagesCount(int entityCount, int entitesPerPage)
            => this.paginationService.CalculatePagesCount(entityCount, entitesPerPage);
    }
}
