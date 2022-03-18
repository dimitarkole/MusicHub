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
    using MusicHub.Data.Models.Enums;
    using MusicHub.Services.Data.Interfaces;
    using MusicHub.Services.Mapping;
    using MusicHub.Web.ViewModels.PlaylistModels;
    using MusicHub.Services.Interfaces;

    public class PlaylistService : IPlaylistService
    {
        private readonly ApplicationDbContext context;
        private readonly IPaginationService paginationService;

        public PlaylistService(
            ApplicationDbContext context,
            IPaginationService paginationService)
        {
            this.context = context;
            this.paginationService = paginationService;
        }

        public PlaylistPageModel<T> All<T>(int page, int entitesPerPage)
        {
            var playlists = this.context.Playlists
                    .Where(p => p.Privacy == Privacy.Public && p.PlaylistMusics.Count > 0)
                    .To<T>();
            return new PlaylistPageModel<T>()
            {
                Playlists = playlists
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetPlaylistsPagesCount(playlists.Count(), entitesPerPage),
            };
        }

        public PlaylistPageModel<T> AllOwned<T>(int page, int entitesPerPage, string userId)
        {
            var playlists = this.context.Playlists
               .Where(p => p.UserId == userId
                    && p.PlaylistMusics.Count > 0)
               .OrderByDescending(p => p.CreatedOn)
               .To<T>();

            return new PlaylistPageModel<T>()
            {
                Playlists = playlists
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetPlaylistsPagesCount(playlists.Count(), entitesPerPage),
            };
        }

        public PlaylistPageModel<T> Filter<T>(int page, int entitesPerPage, PlaylistFilterInputModel model, string userId = null)
        {
            var playlists = this.context.Playlists
               .Where(p => p.PlaylistMusics.Count > 0);

            if (!string.IsNullOrEmpty(userId))
            {
                playlists = playlists.Where(s => s.UserId == userId);
            }
            else
            {
                playlists = playlists.Where(m => m.Privacy == Privacy.Public);
            }

            playlists = this.FilteringPlaylists(model, playlists);
            playlists = this.OrderSearchPlaylist(model.OrderMethod, playlists);
            var playlistsT = playlists.To<T>();
            return new PlaylistPageModel<T>()
            {
                Playlists = playlistsT
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetPlaylistsPagesCount(playlistsT.Count(), entitesPerPage),
            };
        }

        public async Task<string> Create(PlaylistCreateInputModel model, string userId)
        {
            Playlist playlist = model.To<Playlist>();
            playlist.UserId = userId;
            await this.context.Playlists.AddAsync(playlist);
            await this.context.SaveChangesAsync();
            return playlist.Id;
        }

        public async Task Delete(string id)
        {
            Playlist playlist = this.context.Playlists.Find(id);
            this.context.Playlists.Remove(playlist);
            await this.context.SaveChangesAsync();
        }

        public async Task Update(PlaylistEditInputModel model, string id)
        {
            Playlist playlist = this.context.Playlists
                 .FirstOrDefault(s => s.Id == id);

            model.To<Playlist>(playlist);

            this.context.Playlists.Update(playlist);
            await this.context.SaveChangesAsync();
        }

        public T GetById<T>(string id) => this.context.Playlists
            .Where(p => p.Id == id)
            .To<T>()
            .FirstOrDefault();

        public bool IsOwned(string id, string userId)
            => this.context.Playlists
               .Any(p => p.Id == id
                    && p.UserId == userId);

        public IList<T> GetOwnedForAddingMusic<T>(string musicId, string userId)
         => this.context.Playlists
            .Where(p => p.UserId == userId
                 && p.PlaylistMusics.Any(ps => ps.MusicId == musicId) == false)
            .OrderByDescending(ps => ps.Name)
            .To<T>()
            .ToList();

        private IQueryable<Playlist> FilteringPlaylists(PlaylistFilterInputModel filter, IQueryable<Playlist> playlists)
        {
            if (!string.IsNullOrEmpty(filter.UserId))
            {
                playlists = playlists.Where(s => s.UserId == filter.UserId);
            }
            else if (!string.IsNullOrEmpty(filter.Name))
            {
                playlists = playlists.Where(p => p.Name.ToLower().Contains(filter.Name.ToLower()));
            }

            return playlists;
        }

        private IQueryable<Playlist> OrderSearchPlaylist(OrderMethod orderMethod, IQueryable<Playlist> playlists)
        {
            if (orderMethod == OrderMethod.CreatedOnAsc)
            {
                playlists = playlists.OrderBy(s => s.CreatedOn);
            }
            else if (orderMethod == OrderMethod.CreatedOnDesc)
            {
                playlists = playlists.OrderByDescending(s => s.CreatedOn);
            }
            else if (orderMethod == OrderMethod.NameAsc)
            {
                playlists = playlists.OrderBy(s => s.Name);
            }
            else if (orderMethod == OrderMethod.NameDesc)
            {
                playlists = playlists.OrderByDescending(s => s.Name);
            }

            return playlists;
        }

        private int GetPlaylistsPagesCount(int entityCount, int entitesPerPage)
            => this.paginationService.CalculatePagesCount(entityCount, entitesPerPage);
    }
}
