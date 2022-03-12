namespace MusicHub.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using MusicHub.Data.Models;
    using MusicHub.Services.Interfaces;
    using MusicHub.Web.ViewModels.FileModels;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Internal;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using static MusicHub.Common.GlobalConstants;

    public class AzureFileController : ApiController
    {
        private readonly IAzureStorageService azureStorageService;

        public AzureFileController(
            IAzureStorageService azureStorageService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<LogoutModel> logger)
            : base(userManager, signInManager, logger)
        {
            this.azureStorageService = azureStorageService;
        }

        [HttpPost("Documents/{eventId?}")]
        public async Task<IActionResult> CreateDocument(int? eventId, [FromForm] FileInputModel model)
        {
            FileDto file = await this.azureStorageService.Upload(model.File);
            return this.Ok();
        }

        [HttpGet("Documents")]
        public async Task<IActionResult> DownloadDocument()
        {
            var file = new FileDto();
            file.Name = "108216145_687935912055062_1408512723886677269_n2.jpg";

            using (var stream = new MemoryStream())
            {
                await this.azureStorageService.Download(file.Path, stream);
                return this.File(stream.ToArray(), OctetStreamMimeType, file.Name);
            }
        }

        [HttpDelete("Documents/{id}")]
        public async Task<IActionResult> DeleteDocument(string filePath)
        {
            await this.azureStorageService.Delete(filePath);
            return this.Ok();
        }
    }
}
