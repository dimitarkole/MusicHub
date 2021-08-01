namespace MusicHub.Services.Data.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MusicHub.Common;
    using MusicHub.Web.ViewModels.Users;
    using MusicHub.Web.ViewModels.VerificationCodeModels;

    public interface IVerificationCodeService
    {
        Task Create(VerificationCodeCreateInputModel model);

        T GetById<T>(string id);

        T GetActivatedVerificationCode<T>(string userId, VerificationType type);

        bool CheckVerificationCode(string userId, VerificationCodeCheckInputModel model);

        Task Delete(string id);
    }
}
