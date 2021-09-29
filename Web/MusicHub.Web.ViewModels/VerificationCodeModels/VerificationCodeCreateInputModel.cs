namespace MusicHub.Web.ViewModels.VerificationCodeModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using MusicHub.Common;
    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    using static MusicHub.Common.GlobalConstants;
    using static MusicHub.Common.ModelConstants;

    public class VerificationCodeCreateInputModel : IMapTo<VerificationCode>
    {
        public VerificationCodeCreateInputModel()
        {
            Random random = new Random();
            this.Code = new string(Enumerable.Repeat(VerificationCodeConstants.RandomCodeCharacters, VerificationCodeConstants.RandomCodeLength)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public string Code { get; set; }

        [Required]
        public VerificationType Type { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
