namespace MusicHub.Web.ViewModels.Users
{
    using AutoMapper;
    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class UserFullNameViewModel : IHaveCustomMappings
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<User, UserFullNameViewModel>()
                .ForMember(m => m.FullName, y => y.MapFrom(u => $"{u.FirstName} {u.LastName}"));
        }
    }
}
