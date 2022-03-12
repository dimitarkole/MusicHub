using MusicHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MusicHub.Common.GlobalConstants;

namespace MusicHub.Data.Seeding
{
    public class CommentSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var random = new Random();
            var comments = this.GetComments();
            var songs = dbContext.Songs
                .ToArray();
            var userRoleId = dbContext.Roles
              .Where(r => r.Name == Roles.UserRoleName)
              .FirstOrDefault()
              .Id;
            var users = dbContext.Users
                  .Where(u => u.Roles.Any(r => r.RoleId == userRoleId))
                  .ToArray();
            foreach (var comment in comments)
            {
                var songId = songs[random.Next(0, songs.Count())].Id;
                var userId = users[random.Next(0, users.Count())].Id;
                if (!dbContext.Comments.Any(e => e.Text == comment.Text
                    && e.UserId == userId
                    && e.SongId == songId))
                {
                    comment.UserId = userId;
                    comment.SongId = songId;
                    await dbContext.Comments.AddAsync(comment);
                }
            }
        }

        private Comment[] GetComments()
            => new Comment[]
                {
                    new Comment() { Text = "Hi from Bulgaria" },
                    new Comment() { Text = "Fine music" },
                    new Comment() { Text = "This is the best song" },
                    new Comment() { Text = "I like this song!" },
                    new Comment() { Text = "I hate this song!!!" },
                    new Comment() { Text = "What are the best song?" },
                    new Comment() { Text = "Bad song" },
                    new Comment() { Text = "I'am fine" },
                    new Comment() { Text = "This song is amazing." },
                };
    }
}
