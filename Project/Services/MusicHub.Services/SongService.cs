namespace MusicHub.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MusicHub.Common;
    using MusicHub.Common.Extensions;
    using MusicHub.Data;
    using MusicHub.Data.Models;
    using MusicHub.Services.Interfaces;
    using MusicHub.Services.Mapping;
    using MusicHub.Web.ViewModels.SongModels;

    public class SongService : ISongService
    {
        private readonly ApplicationDbContext context;
        private readonly IPaginationService paginationService;

        public SongService(
            ApplicationDbContext context,
            IPaginationService paginationService)
        {
            this.context = context;
            this.paginationService = paginationService;
        }

        public SongsAllViewModel<T> All<T>(int page, int entitesPerPage)
        {
            var songs = this.context.Songs
                    .To<T>();
            return new SongsAllViewModel<T>()
            {
                Songs = songs
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetSongsPagesCount(songs.Count(), entitesPerPage),
            };
        }

        public IEnumerable<T> SuggestSongs<T>(string songId)
        {
            var song = this.context.Songs.Find(songId);
            var suggestSongs = this.context.Songs
                .Where(s => s.Id != songId)
                .OrderByDescending(s => s.CategoryId == song.CategoryId)
                .ThenByDescending(s => s.CreatedOn)
                .ThenByDescending(s => s.SongViewHistories.Count)
                .Take(10)
                .To<T>()
                .ToList();

            return suggestSongs;
        }

        public SongsAllViewModel<T> AllOwn<T>(int page, int entitesPerPage, string userId)
        {
            var songs = this.context.Songs
                    .Where(s => s.UserId == userId)
                    .OrderByDescending(s => s.CreatedOn)
                    .To<T>();
            return new SongsAllViewModel<T>()
            {
                Songs = songs
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetSongsPagesCount(songs.Count(), entitesPerPage),
            };
        }

        public SongsAllViewModel<T> Filter<T>(int page, int entitesPerPage, SongFilter filter, string userId = null)
        {
            IQueryable<Song> songs = this.context.Songs;
            if (!string.IsNullOrEmpty(userId))
            {
                songs = songs.Where(s => s.UserId == userId);
            }

            songs = this.FilteringSongs(filter, songs);

            songs = this.OrederSearchSongs(filter.OrderMethod, songs);
            return new SongsAllViewModel<T>()
                {
                    Songs = songs
                        .To<T>()
                        .GetPage(page, entitesPerPage)
                        .ToList(),
                    CurrentPage = page,
                    NumberOfPages = this.GetSongsPagesCount(songs.Count(), entitesPerPage),
                };
        }

        public async Task<string> Create(SongCreateInputModel model, string userId)
        {
            Song song = model.To<Song>();
            song.UserId = userId;
            await this.context.Songs.AddAsync(song);
            await this.context.SaveChangesAsync();
            return song.Id;
        }

        public async Task Delete(string id)
        {
            Song song = this.context.Songs.Find(id);
            this.context.Songs.Remove(song);
            await this.context.SaveChangesAsync();
        }

        public T GetById<T>(string id)
        {
            var song = this.context.Songs
                .Where(s => s.Id == id)
                .To<T>()
                .FirstOrDefault();

            return song;
        }

        public bool IsOwn(string songId, string userId)
            => this.context.Songs
            .Any(s =>
                s.Id == songId
                && s.UserId == userId);

        public async Task Update(string id, SongEditModel model)
        {
            Song song = this.context.Songs.Find(id);
            song.Name = model.Name;
            song.CategoryId = model.CategoryId;
            song.Text = model.Text;
            song.AudioFilePath = model.AudioFilePath;
            song.ImageFilePath = model.ImageFilePath;
            song.MusicLicenseType = model.MusicLicenseType;
            song.VisibleStatus = model.VisibleStatus;

            this.context.Songs.Update(song);
            await this.context.SaveChangesAsync();
        }

        private IQueryable<Song> FilteringSongs(SongFilter filter, IQueryable<Song> songs)
        {
            if (!string.IsNullOrEmpty(filter.UserId))
            {
                songs = songs.Where(s => s.UserId == filter.UserId);
            }

            if (!string.IsNullOrEmpty(filter.Name))
            {
                songs = songs.Where(s => s.Name.ToLower().Contains(filter.Name.ToLower()));
            }

            if (!string.IsNullOrEmpty(filter.CategoryId))
            {
                songs = songs.Where(s => s.CategoryId == filter.CategoryId);
            }

            if (!string.IsNullOrEmpty(filter.Username))
            {
                songs = songs.Where(s => s.User.UserName.ToLower().Contains(filter.Username.ToLower()));
            }

            return songs;
        }

        private IQueryable<Song> OrederSearchSongs(OrderMethod orderMethod, IQueryable<Song> songs)
        {
            if (orderMethod == OrderMethod.CreatedOnAsc)
            {
                songs = songs.OrderBy(s => s.CreatedOn);
            }
            else if (orderMethod == OrderMethod.CreatedOnDesc)
            {
                songs = songs.OrderByDescending(s => s.CreatedOn);
            }
            else if (orderMethod == OrderMethod.NameAsc)
            {
                songs = songs.OrderBy(s => s.Name);
            }
            else if (orderMethod == OrderMethod.NameDesc)
            {
                songs = songs.OrderByDescending(s => s.Name);
            }

            return songs;
        }

        private int GetSongsPagesCount(int entityCount, int entitesPerPage)
            => this.paginationService.CalculatePagesCount(entityCount, entitesPerPage);
    }
}
