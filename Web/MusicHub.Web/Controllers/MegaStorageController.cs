using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MusicHub.Data.Models;
using MusicHub.Services.Interfaces;
using MusicHub.Web.ViewModels.FileModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static MusicHub.Common.GlobalConstants;

namespace MusicHub.Web.Controllers
{
    public class MegaStorageController : ApiController
    {
        private readonly IMegaStorageService megaStorageService;

        public MegaStorageController(
            IMegaStorageService megaStorageService,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<LogoutModel> logger)
            : base(userManager, signInManager, logger)
        {
            this.megaStorageService = megaStorageService;
        }

        [HttpPost("Documents")]
        public async Task<IActionResult> CreateDocument([FromForm] FileInputModel model)
        {
            FileDto file = await this.megaStorageService.Upload(model.File);
            return this.Ok(file.Path);
        }

        [HttpGet("Documents")]
        public async Task<IActionResult> DownloadDocument()
        {
            var file = new FileDto();
            file.Name = "108216145_687935912055062_1408512723886677269_n2.jpg";

            using (var stream = new MemoryStream())
            {
                await this.megaStorageService.Download(file.Path, stream);
                return this.File(stream.ToArray(), OctetStreamMimeType, file.Name);
            }
        }

        [HttpDelete("Documents/{filePath}")]
        public async Task<IActionResult> DeleteDocument(string filePath)
        {
            await this.megaStorageService.Delete(filePath);
            return this.Ok();
        }
    }
}
