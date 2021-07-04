namespace MusicHub.Web.ViewModels.Users
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class UserChangeRolesInputModel
    {
        public ICollection<RoleIdsViewModel> RoleIds { get; set; }
    }
}
