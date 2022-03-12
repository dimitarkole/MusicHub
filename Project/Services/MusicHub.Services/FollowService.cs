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
    using MusicHub.Web.ViewModels.FollowModels;

    public class FollowService : IFollowService
    {
        private readonly ApplicationDbContext context;

        public FollowService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<T> AllFollowed<T>(string userId)
              => this.context.Followers.Where(f => f.FollowingId == userId)
                  .To<T>()
                  .ToList();

        public IEnumerable<T> AllFollowers<T>(string userId)
            => this.context.Followers.Where(f => f.FollowedId == userId)
                .To<T>()
                .ToList();

        public async Task Create(FollowCreateInputModel model, string userId)
        {
            Follower follow = model.To<Follower>();
            follow.FollowingId = userId;
            await this.context.Followers.AddAsync(follow);
            await this.context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var follow = this.context.Followers
                .Where(f => f.Id == id)
                .FirstOrDefault();
            this.context.Followers.Remove(follow);
            await this.context.SaveChangesAsync();
        }

        public IEnumerable<T> Filter<T>(FollowFilterInputModel filter, string userId)
        {
            var followers = this.context.Followers
                .Where(f => f.FollowingId == userId);
            followers = this.FilteringFollowed(filter, followers);
            followers = this.OrederSearchFollows(filter.OrderMethod, followers);
            return followers.To<T>().ToList();
        }

        public T GetFollow<T>(string followedUserId, string userId)
           => this.context.Followers
                    .Where(f =>
                        f.FollowedId == followedUserId
                        && f.FollowingId == userId)
                    .To<T>()
                    .FirstOrDefault();

        public bool IsFollowed(string followedUserId, string userId)
            => this.context.Followers
                .Any(f =>
                    f.FollowedId == followedUserId
                    && f.FollowingId == userId);

        private IQueryable<Follower> FilteringFollowed(FollowFilterInputModel filter, IQueryable<Follower> follows)
        {
            if (!string.IsNullOrEmpty(filter.FirstName))
            {
                follows = follows.Where(s => s.Followed.FirstName.ToLower().Contains(filter.FirstName.ToLower()));
            }

            if (!string.IsNullOrEmpty(filter.LastName))
            {
                follows = follows.Where(s => s.Followed.LastName.ToLower().Contains(filter.LastName.ToLower()));
            }

            if (!string.IsNullOrEmpty(filter.Username))
            {
                follows = follows.Where(s => s.Followed.UserName.ToLower().Contains(filter.Username.ToLower()));
            }

            return follows;
        }

        private IQueryable<Follower> FilteringFollowedBy(FollowFilterInputModel filter, IQueryable<Follower> follows)
        {
            if (!string.IsNullOrEmpty(filter.FirstName))
            {
                follows = follows.Where(s => s.Followed.FirstName.ToLower().Contains(filter.FirstName.ToLower()));
            }

            if (!string.IsNullOrEmpty(filter.LastName))
            {
                follows = follows.Where(s => s.Followed.LastName.ToLower().Contains(filter.LastName.ToLower()));
            }

            if (!string.IsNullOrEmpty(filter.Username))
            {
                follows = follows.Where(s => s.Followed.UserName.ToLower().Contains(filter.Username.ToLower()));
            }

            return follows;
        }

        private IQueryable<Follower> OrederSearchFollows(OrderMethod orderMethod, IQueryable<Follower> follows)
        {
            /*if (orderMethod == OrderMethod.CreatedOnAsc)
            {
                follows = follows.OrderBy(f => f.CreatedOn);
            }
            else if (orderMethod == OrderMethod.CreatedOnDesc)
            {
                follows = follows.OrderByDescending(f => f.CreatedOn);
            }
            else*/ if (orderMethod == OrderMethod.NameAsc)
            {
                follows = follows.OrderBy(f => f.Following.UserName);
            }
            else if (orderMethod == OrderMethod.NameDesc)
            {
                follows = follows.OrderByDescending(f => f.Following.UserName);
            }

            return follows;
        }
    }
}
