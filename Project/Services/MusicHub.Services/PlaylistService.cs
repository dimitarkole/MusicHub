namespace MusicHub.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Common;
    using MusicHub.Data;
    using MusicHub.Data.Models;
    using MusicHub.Services.Interfaces;
    using MusicHub.Services.Mapping;
    using MusicHub.Web.ViewModels.PlaylistModels;
    using MusicHub.Common.Extensions;

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

        public PlaylistsAllViewModel<T> All<T>(int page, int entitesPerPage)
        {
            var playlists = this.context.Playlists
                    .To<T>();
            return new PlaylistsAllViewModel<T>()
            {
                Playlists = playlists
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetPlaylistsPagesCount(playlists.Count(), entitesPerPage),
            };
        }

        public PlaylistsAllViewModel<T> FilterOwn<T>(int page, int entitesPerPage, PlaylistFilterInputModel model, string userId)
        {
            var playlists = this.context.Playlists
               .Where(p => p.PlaylistSongs.Count > 0
                   && p.UserId == userId);
            playlists = this.FilteringPlaylists(model, playlists);
            playlists = this.OrederSearchPlaylist(model.OrderMethod, playlists);
            var playlistsT = playlists.To<T>();

            return new PlaylistsAllViewModel<T>()
            {
                Playlists = playlistsT
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetPlaylistsPagesCount(playlistsT.Count(), entitesPerPage),
            };
        }

        public PlaylistsAllViewModel<T> Filter<T>(int page, int entitesPerPage, PlaylistFilterInputModel model)
        {
            var playlists = this.context.Playlists
                 .Where(p => p.PlaylistSongs.Count > 0);

            playlists = this.FilteringPlaylists(model, playlists);
            playlists = this.OrederSearchPlaylist(model.OrderMethod, playlists);
            var playlistsT = playlists.To<T>();
            return new PlaylistsAllViewModel<T>()
            {
                Playlists = playlistsT
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetPlaylistsPagesCount(playlistsT.Count(), entitesPerPage),
            };
        }

        public PlaylistsAllViewModel<T> AllOwn<T>(int page, int entitesPerPage, string userId)
        {
            var playlists = this.context.Playlists
               .Where(p => p.UserId == userId
                    && p.PlaylistSongs.Count > 0)
               .OrderByDescending(p => p.CreatedOn)
               .To<T>();

            return new PlaylistsAllViewModel<T>()
            {
                Playlists = playlists
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetPlaylistsPagesCount(playlists.Count(), entitesPerPage),
            };
        }

        public IList<T> GetOwnForAddingSong<T>(string songId, string userId)
            => this.context.Playlists
               .Where(p => p.UserId == userId
                    && p.PlaylistSongs.Any(ps => ps.SongId == songId) == false)
               .OrderByDescending(ps => ps.Name)
               .To<T>()
               .ToList();

        public async Task Create(PlaylistCreateInputModel model, string userId)
        {
            Playlist playlist = model.To<Playlist>();
            playlist.UserId = userId;
            await this.context.Playlists.AddAsync(playlist);
            await this.context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            Playlist playlist = this.context.Playlists.Find(id);
            this.context.Playlists.Remove(playlist);
            await this.context.SaveChangesAsync();
        }

        public T GetById<T>(string id) => this.context.Playlists
               .Where(p => p.Id == id
                    && p.PlaylistSongs.Count > 0)
               .To<T>()
               .FirstOrDefault();

        public bool IsOwn(string id, string userId)
            => this.context.Playlists
               .Any(p => p.Id == id
                    && p.UserId == userId);

        public async Task Update(PlaylistEditInputModel model, string id)
        {
            Playlist playlist = this.context.Playlists
                 .FirstOrDefault(s => s.Id == id);

            model.To<Playlist>(playlist);

            this.context.Playlists.Update(playlist);
            await this.context.SaveChangesAsync();
        }

        private IQueryable<Playlist> FilteringPlaylists(PlaylistFilterInputModel filter, IQueryable<Playlist> playlists)
        {
            playlists = playlists.Where(p => p.PlaylistSongs.Count > 0);
            if (!string.IsNullOrEmpty(filter.UserId))
            {
                playlists = playlists.Where(s => s.UserId == filter.UserId);
            }

            if (!string.IsNullOrEmpty(filter.Name))
            {
                playlists = playlists.Where(p => p.Name.ToLower().Contains(filter.Name.ToLower()));
            }

            return playlists;
        }

        private IQueryable<Playlist> OrederSearchPlaylist(OrderMethod orderMethod, IQueryable<Playlist> playlists)
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
