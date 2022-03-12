namespace MusicHub.Services.Interfaces
{
    using MusicHub.Web.ViewModels.FileModels;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    public interface IAzureStorageService
    {
        Task<string> Upload(Stream stream, string inputFileName);

        Task<FileDto> Upload(IFormFile file);

        Task<string> Upload(byte[] buffer, string fileName);

        Task Download(string filePath, Stream stream);

        Task Delete(string filePath);
    }
}
