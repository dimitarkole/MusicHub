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

    public class MusicService : IMusicService
    {
        private readonly ApplicationDbContext context;
        private readonly IPaginationService paginationService;

        public MusicService(
            ApplicationDbContext context,
            IPaginationService paginationService)
        {
            this.context = context;
            this.paginationService = paginationService;
        }

        public MusicPageModel<T> All<T>(int page, int entitesPerPage)
        {
            var musics = this.context.Musics
                    .Where(m => m.Privacy == Privacy.Public)
                    .OrderByDescending(m => m.CreatedOn)
                    .ThenBy(m => m.MusicReactions.Count(r => r.Reaction == Reaction.Like))
                    .ThenBy(m => m.Name)
                    .To<T>();
            return new MusicPageModel<T>()
            {
                Musics = musics
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetMusicsPagesCount(musics.Count(), entitesPerPage),
            };
        }

        public MusicPageModel<T> AllOwn<T>(int page, int entitesPerPage, string userId)
        {
            var musics = this.context.Musics
                   .Where(s => s.UserId == userId)
                   .OrderByDescending(s => s.CreatedOn)
                   .To<T>();
            return new MusicPageModel<T>()
            {
                Musics = musics
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetMusicsPagesCount(musics.Count(), entitesPerPage),
            };
        }

        public async Task<string> Create(MusicCreateInputModel model, string userId)
        {
            var music = model.To<Music>();
            music.UserId = userId;
            await this.context.Musics.AddAsync(music);
            await this.context.SaveChangesAsync();
            return music.Id;
        }

        public async Task Delete(string id)
        {
            var music = this.context.Musics.Find(id);
            this.context.Musics.Remove(music);
            await this.context.SaveChangesAsync();
        }

        public MusicPageModel<T> Filter<T>(int page, int entitesPerPage, MusicFilter filter, string userId = null)
        {
            IQueryable<Music> musics = this.context.Musics;

            if (!string.IsNullOrEmpty(userId))
            {
                musics = musics.Where(m => m.UserId == userId);
            }
            else
            {
                musics = musics.Where(m => m.Privacy == Privacy.Public);
            }

            musics = this.FilteringMusics(filter, musics);

            musics = this.OrederSearchMusics(filter.OrderMethod, musics);
            return new MusicPageModel<T>()
            {
                Musics = musics
                        .To<T>()
                        .GetPage(page, entitesPerPage)
                        .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetMusicsPagesCount(musics.Count(), entitesPerPage),
            };
        }

        public T GetById<T>(string id)
            => this.context.Musics
                .Where(s => s.Id == id)
                .To<T>()
                .FirstOrDefault();

        public bool IsOwned(string musicId, string userId)
             => this.context.Musics
                .Any(s =>
                    s.Id == musicId
                    && s.UserId == userId);

        public MusicPageModel<T> SuggestMusics<T>(int page, int entitesPerPage, string musicId)
        {
            var music = this.context.Musics.Find(musicId);
            var suggestMusics = this.context.Musics
                .Where(s => s.Id != musicId && s.Privacy == Privacy.Public)
                .OrderByDescending(s => s.CategoryId == music.CategoryId)
                .ThenByDescending(s => s.CreatedOn)
                .ThenByDescending(s => s.PlayedMusics.Count);

            return new MusicPageModel<T>()
            {
                Musics = suggestMusics
                       .To<T>()
                       .GetPage(page, entitesPerPage)
                       .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetMusicsPagesCount(suggestMusics.Count(), entitesPerPage),
            };
        }

        public async Task Update(string id, MusicEditModel model)
        {
            Music music = this.context.Musics.Find(id);
            model.To<Music>(music);
            this.context.Musics.Update(music);
            await this.context.SaveChangesAsync();
        }

        private int GetMusicsPagesCount(int entityCount, int entitesPerPage)
            => this.paginationService.CalculatePagesCount(entityCount, entitesPerPage);

        private IQueryable<Music> OrederSearchMusics(OrderMethod orderMethod, IQueryable<Music> musics)
        {
            musics = musics.OrderByDescending(m => (m.PlayedMusics.Count / 2)
              + ((m.MusicReactions.Count(r => r.Reaction == Reaction.Like)
              - m.MusicReactions.Count(r => r.Reaction == Reaction.Dislike)) * 2)
              + m.Comments.Count());
            /* == 0 /*? 1 : (m.MusicReactions.Count(r => r.Reaction == Reaction.Like)
                           - m.MusicReactions.Count(r => r.Reaction == Reaction.Dislike)
                           + m.Comments.Count()))*/
            /* if (orderMethod == OrderMethod.CreatedOnAsc)
             {
                 musics = musics.OrderBy(s => s.CreatedOn);
             }
             else if (orderMethod == OrderMethod.CreatedOnDesc)
             {
                 musics = musics.OrderByDescending(s => s.CreatedOn);
             }
             else if (orderMethod == OrderMethod.NameAsc)
             {
                 musics = musics.OrderBy(s => s.Name);
             }
             else if (orderMethod == OrderMethod.NameDesc)
             {
                 musics = musics.OrderByDescending(s => s.Name);
             }       */

            return musics;
        }

        private IQueryable<Music> FilteringMusics(MusicFilter filter, IQueryable<Music> musics)
        {
            if (!string.IsNullOrEmpty(filter.UserId))
            {
                musics = musics.Where(s => s.UserId == filter.UserId);
            }

            if (!string.IsNullOrEmpty(filter.Name))
            {
                musics = musics.Where(s => s.Name.ToLower().Contains(filter.Name.ToLower()));
            }

            if (!string.IsNullOrEmpty(filter.CategoryId))
            {
                musics = musics.Where(s => s.CategoryId == filter.CategoryId);
            }

            if (!string.IsNullOrEmpty(filter.Username))
            {
                musics = musics.Where(s => s.User.UserName.ToLower().Contains(filter.Username.ToLower()));
            }

            return musics;
        }
    }
}
