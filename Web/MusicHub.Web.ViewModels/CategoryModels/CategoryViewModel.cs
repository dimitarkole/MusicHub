namespace MusicHub.Web.ViewModels.CategoryModels
{
    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class CategoryViewModel : IMapFrom<Category>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
