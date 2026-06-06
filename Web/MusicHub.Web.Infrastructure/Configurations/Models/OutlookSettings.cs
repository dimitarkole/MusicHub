namespace MusicHub.Web.Infrastucture.Configurations.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class OutlookSettings
    {
        public string ApiKey { get; set; }

        public string Scopes { get; set; }

        public string TenentId { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string RedirectUri { get; set; }
    }
}

