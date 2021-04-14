using Microsoft.AspNetCore.Http;
using MusicHub.Web.ViewModels.FileModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MusicHub.Services.Interfaces
{
    public interface IMegaStorageService
    {
        Task<string> Upload(Stream stream, string fileName);

        Task<FileDto> Upload(IFormFile file);

        Task<Stream> Download(FileDowloadInputModel model, Stream stream);

        Task Delete(string filePath);
    }
}
