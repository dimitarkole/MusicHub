namespace MusicHub.Web.Infrastucture.Configurations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MusicHub.Common.Settings;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class SettingsConfiguration
    {
        public static IServiceCollection AddApplicationSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSettings<EmailSettings>(nameof(EmailSettings), configuration);
            services.AddSettings<AzureBlobSettings>(nameof(AzureBlobSettings), configuration);
            services.AddSettings<ActiveDirectorySettings>(nameof(ActiveDirectorySettings), configuration);
            services.AddSettings<AppSettings>(nameof(AppSettings), configuration);
            return services;
        }

        public static IServiceCollection AddSettings<T>(this IServiceCollection services, string sectionName, IConfiguration configuration)
            where T : class
        {
            T settings = configuration
               .GetSection(sectionName)
               .Get<T>();

            if (settings != null)
            {
                services.AddSingleton(settings);
            }

            return services;
        }
    }
}
