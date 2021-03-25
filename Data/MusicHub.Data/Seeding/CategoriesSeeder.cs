namespace MusicHub.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Data.Models;

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
                    new Category() { Name = "Roc", Musics = new List<Music>() },
                    new Category() { Name = "Rap", Musics = new List<Music>() },
                    new Category() { Name = "R&B", Musics = new List<Music>() },
                    new Category() { Name = "Chalga", Musics = new List<Music>() },
                    new Category() { Name = "Pop", Musics = new List<Music>() },
                    new Category() { Name = "Folk", Musics = new List<Music>() },
                };
    }
}
