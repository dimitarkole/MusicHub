namespace MusicHub.Web.ViewModels.PermissionModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class PermissionViewModel : IMapFrom<Permission>
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }
    }
}
