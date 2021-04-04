using AutoMapper;
using MusicHub.Data.Models;
using MusicHub.Services.Mapping;
using System.Linq;

namespace MusicHub.Web.ViewModels.Users
{
    public class UserEditInputModel : UserInputModel, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string[] Roles { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<User, UserEditInputModel>()
                 .ForMember(m => m.Roles, y => y.MapFrom(u => u.Roles.Select(r => r.Role.Name).ToList()));

            configuration.CreateMap<UserEditInputModel, User>()
                .ForMember(u => u.Roles, y => y.Ignore())
                .ForMember(u => u.Id, y => y.Ignore());
        }
    }
}
