namespace MusicHub.Web.ViewModels.PermissionModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.Services.Mapping;

    public class PermissionDetailsModel : IMapFrom<Permission>
    {
        public PermissionType Type { get; set; }

        public string Description { get; set; }
    }
}
