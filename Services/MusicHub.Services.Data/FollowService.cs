namespace MusicHub.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Common;
    using MusicHub.Data;
    using MusicHub.Data.Models;
    using MusicHub.Services.Data.Interfaces;
    using MusicHub.Services.Mapping;
    using MusicHub.Web.ViewModels.FollowModels;
    using MusicHub.Common.Extensions;

    public class FollowService : IFollowService
    {
        private readonly ApplicationDbContext context;
        private readonly IPaginationService paginationService;

        public FollowService(ApplicationDbContext context,
            IPaginationService paginationService)
        {
            this.context = context;
            this.paginationService = paginationService;
        }

        public FollowingsAllViewModel<T> AllFollowing<T>(int page, int entitesPerPage, string userId)
        {
            var followings = this.context.Follows
                .Where(f => f.FollowerId == userId)
                .To<T>()
                .ToList();

            return new FollowingsAllViewModel<T>()
            {
                Followings = followings
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetFollowPagesCount(followings.Count(), entitesPerPage),
            };
        }

        public FollowingsAllViewModel<T> FilterFollowings<T>(int page, int entitesPerPage, FollowFilterInputModel filter, string userId)
        {
            var followings = this.context.Follows
               .Where(f => f.FollowerId == userId);

            followings = this.FilteringFollowings(filter, followings);
            followings = this.OrederFollowings(filter.OrderMethod, followings);
            var followersT = followings.To<T>();

            return new FollowingsAllViewModel<T>()
            {
                Followings = followersT
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetFollowPagesCount(followings.Count(), entitesPerPage),
            };
        }

        public FollowersAllViewModel<T> AllFollowers<T>(int page, int entitesPerPage, string userId)
        {
            var followers = this.context.Follows
                .Where(f => f.FollowingId == userId)
                .To<T>();

            return new FollowersAllViewModel<T>()
            {
                Followers = followers
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetFollowPagesCount(followers.Count(), entitesPerPage),
            };
        }

        public FollowersAllViewModel<T> FilterFollowers<T>(int page, int entitesPerPage, FollowFilterInputModel filter, string userId)
        {
            var followers = this.context.Follows
               .Where(f => f.FollowingId == userId);
            followers = this.FilteringFollowers(filter, followers);
            followers = this.OrederFollows(filter.OrderMethod, followers);
            var followersT = followers.To<T>();
            return new FollowersAllViewModel<T>()
            {
                Followers = followersT
                    .GetPage(page, entitesPerPage)
                    .ToList(),
                CurrentPage = page,
                NumberOfPages = this.GetFollowPagesCount(followersT.Count(), entitesPerPage),
            };
        }

        public async Task Create(FollowCreateInputModel model, string userId)
        {
            Follow follow = model.To<Follow>();
            follow.FollowingId = userId;
            await this.context.Follows.AddAsync(follow);
            await this.context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var follow = this.context.Follows
                .Where(f => f.Id == id)
                .FirstOrDefault();
            this.context.Follows.Remove(follow);
            await this.context.SaveChangesAsync();
        }

        public T GetFollow<T>(string followingUserId, string userId)
           => this.context.Follows
                    .Where(f =>
                        f.FollowingId == followingUserId
                        && f.FollowerId == userId)
                    .To<T>()
                    .FirstOrDefault();

        public bool IsFollowing(string followerId, string userId)
            => this.context.Follows
                .Any(f => f.FollowerId == followerId
                    && f.FollowingId == userId);

        public bool IsOwned(string id, string userId)
           => this.context.Follows
                .Any(f =>
                    f.Id == id
                    && f.FollowingId == userId);

        private IQueryable<Follow> FilteringFollowings(FollowFilterInputModel filter, IQueryable<Follow> follows)
        {
            if (!string.IsNullOrEmpty(filter.FirstName))
            {
                follows = follows.Where(s => s.Following.FirstName.ToLower().Contains(filter.FirstName.ToLower()));
            }

            if (!string.IsNullOrEmpty(filter.LastName))
            {
                follows = follows.Where(s => s.Following.LastName.ToLower().Contains(filter.LastName.ToLower()));
            }

            if (!string.IsNullOrEmpty(filter.Username))
            {
                follows = follows.Where(s => s.Following.UserName.ToLower().Contains(filter.Username.ToLower()));
            }

            return follows;
        }

        private IQueryable<Follow> FilteringFollowers(FollowFilterInputModel filter, IQueryable<Follow> follows)
        {
            if (!string.IsNullOrEmpty(filter.FirstName))
            {
                follows = follows.Where(s => s.Follower.FirstName.ToLower().Contains(filter.FirstName.ToLower()));
            }

            if (!string.IsNullOrEmpty(filter.LastName))
            {
                follows = follows.Where(s => s.Follower.LastName.ToLower().Contains(filter.LastName.ToLower()));
            }

            if (!string.IsNullOrEmpty(filter.Username))
            {
                follows = follows.Where(s => s.Follower.UserName.ToLower().Contains(filter.Username.ToLower()));
            }

            return follows;
        }

        private IQueryable<Follow> OrederFollows(OrderMethod orderMethod, IQueryable<Follow> follows)
        {
            if (orderMethod == OrderMethod.NameAsc)
            {
                follows = follows.OrderBy(f => f.Follower.UserName);
            }
            else if (orderMethod == OrderMethod.NameDesc)
            {
                follows = follows.OrderByDescending(f => f.Follower.UserName);
            }

            return follows;
        }

        private IQueryable<Follow> OrederFollowings(OrderMethod orderMethod, IQueryable<Follow> follows)
        {
            if (orderMethod == OrderMethod.NameDesc)
            {
                follows = follows.OrderByDescending(f => f.Following.UserName);
            }
            else
            {
                follows = follows.OrderBy(f => f.Following.UserName);
            }

            return follows;
        }

        private int GetFollowPagesCount(int entityCount, int entitesPerPage)
            => this.paginationService.CalculatePagesCount(entityCount, entitesPerPage);
    }
}
