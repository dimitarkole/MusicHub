namespace MusicHub.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var categories = this.GetCategories().OrderBy(x => x.Name).ToArray();

            foreach (var category in categories)
            {
                if (!dbContext.Categories.Any(e => e.Name == category.Name))
                {
                    await dbContext.Categories.AddAsync(category);
                }
            }
        }

        private Category[] GetCategories()
            => new Category[]
                {
                    new Category() { Name = "Roc", Songs = new List<Song>() },
                    new Category() { Name = "Rap", Songs = new List<Song>() },
                    new Category() { Name = "R&B", Songs = new List<Song>() },
                    new Category() { Name = "Chalga", Songs = new List<Song>() },
                    new Category() { Name = "Pop", Songs = new List<Song>() },
                    new Category() { Name = "Folk", Songs = new List<Song>() },
                };
    }
}
