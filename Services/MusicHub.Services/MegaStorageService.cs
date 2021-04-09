using CG.Web.MegaApiClient;
using Microsoft.AspNetCore.Http;
using MusicHub.Common;
using MusicHub.Services.Interfaces;
using MusicHub.Web.ViewModels.FileModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHub.Services
{
    public class MegaStorageService : IMegaStorageService
    {
        private readonly MegaApiClient client;


        public MegaStorageService()
        {
            this.client = new MegaApiClient();
            this.client.Login(GlobalConstants.MegaStorageData.Email, GlobalConstants.MegaStorageData.Password);
        }

        public async Task Delete(string filePath)
        {
            Uri fileLink = new Uri(filePath);
            var nodes = await this.client.GetNodesFromLinkAsync(fileLink);
            var node = nodes.FirstOrDefault();

            await this.client.DeleteAsync(node);
        }

        public async Task<Stream> Download(string filePath, Stream stream)
        {
            this.client.LoginAnonymous();

            Uri fileLink = new Uri(filePath);
            var nodes = await this.client.GetNodesFromLinkAsync(fileLink);
            var node = nodes.FirstOrDefault();

            stream = await this.client.DownloadAsync(node);

            this.client.Logout();
            return stream;
        }

        public async Task<string> Upload(Stream stream, string fileName)
        {
            string filePath = this.GenerateUniqueFileName(fileName);

            IEnumerable<INode> nodes = this.client.GetNodes();

            INode root = nodes.Single(x => x.Type == NodeType.Root);
            INode myFolder = nodes.Where(n => n.Name == "Upload").FirstOrDefault();

            INode uploadFile = await this.client.UploadAsync(stream, fileName, myFolder);
            Uri downloadLink = this.client.GetDownloadLink(uploadFile);

            this.client.Logout();

            return downloadLink.ToString();
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

        private string GenerateUniqueFileName(string fileName) => $"{Guid.NewGuid()}_{fileName}";
    }
}
