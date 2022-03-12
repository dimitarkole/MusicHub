namespace MusicHub.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Services.Interfaces;
    using MusicHub.Web.ViewModels.FileModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.WindowsAzure.Storage.Blob;

    public class AzureStorageService : IAzureStorageService
    {
        private readonly CloudBlobContainer cloudBlobContainer;

        public AzureStorageService(CloudBlobContainer cloudBlobContainer)
        {
            this.cloudBlobContainer = cloudBlobContainer;
        }

        public async Task<string> Upload(Stream stream, string fileName)
        {
            string filePath = this.GenerateUniqueFileName(fileName);

            CloudBlockBlob cloudBlockBlob = await this.GetCloudBlockBlob(filePath);
            await cloudBlockBlob.UploadFromStreamAsync(stream);

            return filePath;
        }

        public async Task<string> Upload(byte[] buffer, string fileName)
        {
            string filePath = this.GenerateUniqueFileName(fileName);

            CloudBlockBlob cloudBlockBlob = await this.GetCloudBlockBlob(filePath);
            await cloudBlockBlob.UploadFromByteArrayAsync(buffer, 0, buffer.Length);

            return filePath;
        }

        public async Task<FileDto> Upload(IFormFile file)
        {
            var fileDto = new FileDto
            {
                Name = file.FileName,
                Type = file.ContentType,
                Size = file.Length,
            };

            using (Stream stream = file.OpenReadStream())
            {
                fileDto.Path = await this.Upload(stream, file.FileName);
            }

            return fileDto;
        }

        public async Task Download(string filePath, Stream stream)
        {
            CloudBlockBlob cloudBlockBlob = await this.GetCloudBlockBlob(filePath);
            await cloudBlockBlob.DownloadToStreamAsync(stream);
        }

        public async Task Delete(string filePath)
        {
            CloudBlockBlob cloudBlockBlob = await this.GetCloudBlockBlob(filePath);
            await cloudBlockBlob.DeleteIfExistsAsync();
        }

        private async Task<CloudBlockBlob> GetCloudBlockBlob(string filePath)
        {
            CloudBlobContainer cloudBlobContainer = await this.ConfigureContainer();
            CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(filePath);
            return cloudBlockBlob;
        }

        private async Task<CloudBlobContainer> ConfigureContainer()
        {
            await this.cloudBlobContainer.CreateIfNotExistsAsync();

            var permissions = new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob,
            };
            await this.cloudBlobContainer.SetPermissionsAsync(permissions);

            return this.cloudBlobContainer;
        }

        private string GenerateUniqueFileName(string fileName) => $"{Guid.NewGuid()}_{fileName}";
    }
}
