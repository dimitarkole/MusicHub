namespace MusicHub.Web.ViewModels.FileModels
{
    using System.ComponentModel.DataAnnotations;

    public class DeleteFileInputModel
    {
        [Required]
        public string FileName { get; set; }

        [Required]
        public string Path { get; set; }
    }
}
