namespace MusicHub.Web.ViewModels.VerificationCodeModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using MusicHub.Data.Models;
    using MusicHub.Services.Mapping;

    public class VerificationCodeViewModel : IMapFrom<VerificationCode>
    {
        public string Id { get; set; }

        public string Code { get; set; }

        public string UserId { get; set; }

        public string IsUsed { get; set; }
    }
}
