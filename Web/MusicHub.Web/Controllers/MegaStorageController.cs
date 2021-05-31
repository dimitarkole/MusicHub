namespace MusicHub.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using MusicHub.Data.Models;
    using MusicHub.Services.Interfaces;
    using MusicHub.Web.ViewModels.FileModels;

    using static MusicHub.Common.GlobalConstants;

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

        [HttpPost]
        public async Task<ActionResult<string>> CreateDocument([FromForm] FileInputModel model)
        {
            FileDto file = await this.megaStorageService.Upload(model.File);
            return this.StatusCode(StatusCodes.Status201Created, new { filePath = file.Path });
        }

        [HttpPost("Download")]
        public async Task<IActionResult> Download(FileDowloadInputModel model)
        {
            using (var stream = new MemoryStream())
            {
                await this.megaStorageService.Download(model, stream);
                return this.File(stream.ToArray(), OctetStreamMimeType, model.FileUrl);
            }
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteDocument(FileDowloadInputModel model)
        {
            await this.megaStorageService.Delete(model.FileUrl);
            return this.Ok();
        }
    }
}
