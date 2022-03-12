namespace MusicHub.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using MusicHub.Data.Models;
    using MusicHub.Web.ViewModels.FileModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Internal;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public class FileController : ApiController
    {
        public FileController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<LogoutModel> logger)
            : base(userManager, signInManager, logger)
        {
        }

        /// <summary>Create file.</summary>
        /// <param name="model">File data.
        /// <para>File - file.</para>
        /// <para>Path - folder path.</para>
        /// </param>
        /// <returns>Ok(fileName).</returns>
        [DisableRequestSizeLimit]
        [HttpPost]
        public IActionResult Create([FromForm] FileInputModel model)
            => this.Ok(new { fileName = this.Upload(model.File, model.Path) });

        /// <summary>Create list of file.</summary>
        /// <param name="model">File data.
        /// <para>File - file.</para>
        /// <para>Path - folder path.</para>
        /// </param>
        /// <returns>Ok(fileName).</returns>
        [DisableRequestSizeLimit]
        [HttpPost(nameof(CreateList))]
        public IActionResult CreateList([FromForm] List<FileInputModel> model)
        {
            var result = new List<object>();
            foreach (var file in model)
            {
                result.Add(new { fileName = this.Upload(file.File, file.Path) });
            }

            return this.Ok(result);
        }

        /// <summary>Delete file.</summary>
        /// <param name="model">File data.
        /// <para>FileName - name of file.</para>
        /// <para>Path - folder path.</para>
        /// </param>
        /// <returns>Ok(fileName).</returns>
        [HttpPost(nameof(Delete))]
        public IActionResult Delete(DeleteFileInputModel model)
        {
            this.DeleteFile(model.FileName, model.Path);
            return this.Ok();
        }

        /// <summary>Upload file to currext folder.</summary>
        /// <param name="fileName">Name of file.</param>
        /// <param name="path">Path - folder path.</param>
        /// <returns>uniqueName.</returns>
        private string Upload(IFormFile file, string path)
        {
            string extension = Path.GetExtension(file.FileName);
            var fileName = Path.GetFileName(file.FileName);
            fileName = fileName.Substring(0, fileName.Length - extension.Length);
            fileName.Replace(' ', '_');
            var uniqueName = fileName + "_" + this.RandomName();
            var folderName = Path.Combine("ClientApp", "src", "assets", "resources", path);
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            uniqueName += extension;
            var fullPath = Path.Combine(pathToSave, uniqueName);
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return uniqueName;
        }

        /// <summary>Remove file from system.</summary>
        /// <param name="fileName">Name of file.</param>
        /// <param name="path">Path - folder path.</param>
        /// <returns>Ok(fileName).</returns>
        private void DeleteFile(string fileName, string path)
        {
            var filePath = Path.Combine("client", "src", "assets", "resources", path, fileName);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }

        /// <summary>Generata random name.</summary>
        /// <returns>uniqueName.</returns>
        private string RandomName()
        {
            int length = Common.GlobalConstants.CreateFile.RandomNameLength;
            Random random = new Random();
            const string chars = Common.GlobalConstants.CreateFile.RandomNameCharacters;
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
