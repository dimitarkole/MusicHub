namespace MusicHub.Services
{
    using System.Linq;
    using System.Threading.Tasks;

    using MusicHub.Common;
    using MusicHub.Common.Extensions;
    using MusicHub.Data;
    using MusicHub.Data.Models;
    using MusicHub.Services.Interfaces;
    using MusicHub.Services.Mapping;
    using MusicHub.Web.ViewModels.SongModels;
    using MusicHub.Web.ViewModels.SongReactionModels;

    public class SongReactionService : ISongReactionService
    {
        private readonly ApplicationDbContext context;
        private readonly IPaginationService paginationService;

        public SongReactionService(
            ApplicationDbContext context,
            IPaginationService paginationService)
        {
            this.context = context;
            this.paginationService = paginationService;
        }

        public LikedSongsAllViewModel<T> All<T>(int page, int entitesPerPage, string userId)
        {
            var likedSongs = this.context.SongReactions
                .Where(s => s.UserId == userId
                    && s.Reaction == Reaction.Like)
                .OrderByDescending(s => s.CreatedOn)
                .To<T>();

            return new LikedSongsAllViewModel<T>()
            {
                LinkedSongs = likedSongs
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetLikedSongsPagesCount(likedSongs.Count(), entitesPerPage),
            };
        }

        public async Task Create(SongReactionCreateInputModel model, string userId)
        {
            var songReaction = model.To<SongReaction>();
            songReaction.UserId = userId;
            songReaction.SongId = model.SongId;
            await this.context.SongReactions.AddAsync(songReaction);
            await this.context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var songReaction = this.context.SongReactions.Find(id);
            this.context.SongReactions.Remove(songReaction);
            await this.context.SaveChangesAsync();
        }

        public LikedSongsAllViewModel<T> Filter<T>(int page, int entitesPerPage, string userId, SongFilter filter)
        {
            var likedSongs = this.context.SongReactions
                .Where(s => s.UserId == userId
                    && s.Reaction == Reaction.Like);

            likedSongs = this.FilteringSongs(filter, likedSongs);
            likedSongs = this.OrederSearchSongs(filter.OrderMethod, likedSongs);

            return new LikedSongsAllViewModel<T>()
            {
                LinkedSongs = likedSongs
                    .GetPage(page, entitesPerPage)
                    .To<T>()
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetLikedSongsPagesCount(likedSongs.Count(), entitesPerPage),
            };
        }

        public SongReactionViewModel GetOwnReaction(string songId, string userId)
        {
            var result = this.context.SongReactions
                .Where(r => r.UserId == userId
                    && r.SongId == songId)
                .To<SongReactionViewModel>()
                .FirstOrDefault();

            if (result == null)
            {
                return new SongReactionViewModel()
                {
                    Reaction = Reaction.None,
                };
            }

            return result;
        }

        public async Task Update(SongReactionCreateInputModel model, string id)
        {
            var songReaction = this.context.SongReactions.Find(id);
            songReaction.Reaction = model.Reaction;

            this.context.SongReactions.Update(songReaction);
            await this.context.SaveChangesAsync();
        }

        private IQueryable<SongReaction> FilteringSongs(SongFilter filter, IQueryable<SongReaction> songsReaction)
        {
            if (!string.IsNullOrEmpty(filter.UserId))
            {
                songsReaction = songsReaction.Where(s => s.UserId == filter.UserId);
            }

            if (!string.IsNullOrEmpty(filter.Name))
            {
                songsReaction = songsReaction.Where(s => s.Song.Name.ToLower().Contains(filter.Name.ToLower()));
            }

            if (!string.IsNullOrEmpty(filter.CategoryId))
            {
                songsReaction = songsReaction.Where(s => s.Song.CategoryId == filter.CategoryId);
            }

            if (!string.IsNullOrEmpty(filter.Username))
            {
                songsReaction = songsReaction.Where(s => s.Song.User.UserName.ToLower().Contains(filter.Username.ToLower()));
            }

            return songsReaction;
        }

        private IQueryable<SongReaction> OrederSearchSongs(OrderMethod orderMethod, IQueryable<SongReaction> songs)
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
                songs = songs.OrderBy(s => s.Song.Name);
            }
            else if (orderMethod == OrderMethod.NameDesc)
            {
                songs = songs.OrderByDescending(s => s.Song.Name);
            }

            return songs;
        }

        private int GetLikedSongsPagesCount(int entityCount, int entitesPerPage)
            => this.paginationService.CalculatePagesCount(entityCount, entitesPerPage);
    }
}
