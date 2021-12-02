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
    using MusicHub.Services.Interfaces;
    using MusicHub.Services.Mapping;
    using MusicHub.Web.ViewModels.MusicModels;
    using MusicHub.Web.ViewModels.MusicReactionModels;

    public class MusicReactionService : IMusicReactionService
    {
        private readonly ApplicationDbContext context;
        private readonly IPaginationService paginationService;

        public MusicReactionService(
            ApplicationDbContext context,
            IPaginationService paginationService)
        {
            this.context = context;
            this.paginationService = paginationService;
        }

        public LikedMusicsAllViewModel<T> All<T>(int page, int entitesPerPage, string userId)
        {
            var likedMusics = this.context.MusicReactions
              .Where(s => s.UserId == userId
                  && s.Reaction == Reaction.Like)
              .OrderByDescending(s => s.CreatedOn)
              .To<T>();

            return new LikedMusicsAllViewModel<T>()
            {
                LikedMusics = likedMusics
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetLikedMusicPagesCount(likedMusics.Count(), entitesPerPage),
            };
        }

        public async Task Create(MusicReactionCreateInputModel model, string userId)
        {
            var musicReaction = model.To<MusicReaction>();
            musicReaction.UserId = userId;
            await this.context.MusicReactions.AddAsync(musicReaction);
            await this.context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var musicReaction = this.context.MusicReactions.Find(id);
            this.context.MusicReactions.Remove(musicReaction);
            await this.context.SaveChangesAsync();
        }

        public LikedMusicsAllViewModel<T> Filter<T>(int page, int entitesPerPage, string userId, MusicFilter filter)
        {
            var likedMusics = this.context.MusicReactions
                .Where(s => s.UserId == userId
                    && s.Reaction == Reaction.Like);

            likedMusics = FilteringMusics(filter, likedMusics);
            likedMusics = OrederSearchMusics(filter.OrderMethod, likedMusics);

            return new LikedMusicsAllViewModel<T>()
            {
                LikedMusics = likedMusics
                    .GetPage(page, entitesPerPage)
                    .To<T>()
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetLikedMusicPagesCount(likedMusics.Count(), entitesPerPage),
            };
        }

        public MusicReactionViewModel GetOwnReaction(string musicId, string userId)
        {
            var result = this.context.MusicReactions
              .Where(r => r.UserId == userId
                  && r.MusicId == musicId)
              .To<MusicReactionViewModel>()
              .FirstOrDefault();

            if (result == null)
            {
                return new MusicReactionViewModel()
                {
                    Reaction = Reaction.None,
                };
            }

            return result;
        }

        public async Task Update(MusicReactionCreateInputModel model, string id)
        {
            var musicReaction = this.context.MusicReactions.Find(id);
            musicReaction.Reaction = model.Reaction;

            this.context.MusicReactions.Update(musicReaction);
            await this.context.SaveChangesAsync();
        }

        public bool IsOwned(string id, string userId)
            => this.context.MusicReactions
                .Any(s => s.Id == id
                    && s.UserId == userId);

        private static IQueryable<MusicReaction> FilteringMusics(MusicFilter filter, IQueryable<MusicReaction> musicReactions)
        {
            if (!string.IsNullOrEmpty(filter.UserId))
            {
                musicReactions = musicReactions.Where(s => s.UserId == filter.UserId);
            }

            if (!string.IsNullOrEmpty(filter.Name))
            {
                musicReactions = musicReactions.Where(s => s.Music.Name.ToLower().Contains(filter.Name.ToLower()));
            }

            if (!string.IsNullOrEmpty(filter.CategoryId))
            {
                musicReactions = musicReactions.Where(s => s.Music.CategoryId == filter.CategoryId);
            }

            if (!string.IsNullOrEmpty(filter.Username))
            {
                musicReactions = musicReactions.Where(s => s.Music.User.UserName.ToLower().Contains(filter.Username.ToLower()));
            }

            return musicReactions;
        }

        private static IQueryable<MusicReaction> OrederSearchMusics(OrderMethod orderMethod, IQueryable<MusicReaction> musicReactions)
        {
            if (orderMethod == OrderMethod.CreatedOnAsc)
            {
                musicReactions = musicReactions.OrderBy(s => s.CreatedOn);
            }
            else if (orderMethod == OrderMethod.CreatedOnDesc)
            {
                musicReactions = musicReactions.OrderByDescending(s => s.CreatedOn);
            }
            else if (orderMethod == OrderMethod.NameAsc)
            {
                musicReactions = musicReactions.OrderBy(s => s.Music.Name);
            }
            else if (orderMethod == OrderMethod.NameDesc)
            {
                musicReactions = musicReactions.OrderByDescending(s => s.Music.Name);
            }

            return musicReactions;
        }

        private int GetLikedMusicPagesCount(int entityCount, int entitesPerPage)
          => this.paginationService.CalculatePagesCount(entityCount, entitesPerPage);
    }
}
