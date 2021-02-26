using AutoMapper;
using MusicHub.Data.Models;
using MusicHub.Services.Mapping;
using System.ComponentModel.DataAnnotations;

namespace MusicHub.Web.ViewModels.Users
{
    public class UserCreateInputModel : UserInputModel, IHaveCustomMappings
    {
        public string Password { get; set; }
        public bool IsAd { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<UserCreateInputModel, User>()
                .ForMember(u => u.Roles, y => y.Ignore());
        }
    }
}
