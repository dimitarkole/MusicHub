namespace MusicHub.Web.Infrastucture.Configurations
{
    using AutoMapper.Configuration;
    using MusicHub.Common;
    using MusicHub.Common.Settings;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public static class AzureBlobStorageConfiguration
    {
        public static IServiceCollection ConfigureAzureBlobStorage(this IServiceCollection services)
        {
            AzureBlobSettings azureBlobSettings = services.BuildServiceProvider().GetService<AzureBlobSettings>();

            //If someone try to start the application but have no azure storage account, just will skip adding azure storage related services to the DI container
            if (azureBlobSettings == null || string.IsNullOrEmpty(azureBlobSettings.StorageConnectionString))
            {
                return services;
            }

            var storageAccount = CloudStorageAccount.Parse(azureBlobSettings.StorageConnectionString);
            services.AddSingleton(storageAccount);

            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(azureBlobSettings.ContainerName);

            services.AddSingleton(cloudBlobContainer);
            return services;
        }
    }
}
