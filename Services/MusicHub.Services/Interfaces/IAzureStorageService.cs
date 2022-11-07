using Microsoft.AspNetCore.Http;
using MusicHub.Web.ViewModels.FileModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHub.Services.Interfaces
{
    public interface IAzureStorageService
    {
        Task<string> Upload(Stream stream, string inputFileName);

        Task<FileDto> Upload(IFormFile file);

        Task<string> Upload(byte[] buffer, string fileName);

        Task Download(string filePath, Stream stream);

        Task Delete(string filePath);
    }
}
