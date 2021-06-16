using CG.Web.MegaApiClient;
using Microsoft.Extensions.DependencyInjection;
using MusicHub.Common.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicHub.Web.Infrastucture.Configurations
{
    public static class МегаStorageConfiguration
    {
        public static IServiceCollection ConfigureМегаStorage(this IServiceCollection services)
        {
            MegaStorageSettings megaStorageSettings = services.BuildServiceProvider().GetService<MegaStorageSettings>();

            //If someone try to start the application but have no azure storage account, just will skip adding azure storage related services to the DI container
            if (megaStorageSettings == null
                || string.IsNullOrEmpty(megaStorageSettings.Email)
                || string.IsNullOrEmpty(megaStorageSettings.Password))
            {
                return services;
            }

            var storageAccount = new MegaApiClient();
            services.AddSingleton(storageAccount);

            return services;
        }
    }
}
