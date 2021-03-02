using MusicHub.Common;
using MusicHub.Data;
using MusicHub.Data.Models;
using MusicHub.Services.Data.Interfaces;
using MusicHub.Web.ViewModels.FollowModels;
using System;
using System.Collections.Generic;
using MusicHub.Services.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHub.Services.Data
{
    public class FollowService : IFollowService
    {
        private readonly ApplicationDbContext context;

        public FollowService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<T> AllFollowed<T>(string userId)
              => this.context.Follows.Where(f => f.FollowerId == userId)
                  .To<T>()
                  .ToList();

        public IEnumerable<T> AllFollowers<T>(string userId)
            => this.context.Follows.Where(f => f.FollowedId == userId)
                .To<T>()
                .ToList();

        public async Task Create(FollowCreateInputModel model, string userId)
        {
            Follow follow = model.To<Follow>();
            follow.FollowerId = userId;
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

        public IEnumerable<T> Filter<T>(FollowFilterInputModel filter, string userId)
        {
            var followers = this.context.Follows
                .Where(f => f.FollowerId == userId);
            followers = this.FilteringFollowed(filter, followers);
            followers = this.OrederSearchFollows(filter.OrderMethod, followers);
            return followers.To<T>().ToList();
        }

        public T GetFollow<T>(string followedUserId, string userId)
           => this.context.Follows
                    .Where(f =>
                        f.FollowedId == followedUserId
                        && f.FollowerId == userId)
                    .To<T>()
                    .FirstOrDefault();

        public bool IsFollowed(string followedUserId, string userId)
            => this.context.Follows
                .Any(f =>
                    f.FollowedId == followedUserId
                    && f.FollowerId == userId);

        private IQueryable<Follow> FilteringFollowed(FollowFilterInputModel filter, IQueryable<Follow> follows)
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

        private IQueryable<Follow> FilteringFollowedBy(FollowFilterInputModel filter, IQueryable<Follow> follows)
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

        private IQueryable<Follow> OrederSearchFollows(OrderMethod orderMethod, IQueryable<Follow> follows)
        {
            /*if (orderMethod == OrderMethod.CreatedOnAsc)
            {
                follows = follows.OrderBy(f => f.CreatedOn);
            }
            else if (orderMethod == OrderMethod.CreatedOnDesc)
            {
                follows = follows.OrderByDescending(f => f.CreatedOn);
            }
            else*/
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
    }
}
