namespace MusicHub.Web.ViewModels.ProfilleModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class UserFullNameViewModel : IHaveCustomMappings
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, UserFullNameViewModel>()
                .ForMember(m => m.FullName, y => y.MapFrom(u => $"{u.FirstName} {u.LastName}"));
        }
    }
}
