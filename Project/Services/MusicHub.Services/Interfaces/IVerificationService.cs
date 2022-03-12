namespace MusicHub.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Web.ViewModels.VerificationCodeModels;

    public interface IVerificationService
    {
        Task Create(VerificationCodeCreateInputModel model);

        bool Check(VerificationCodeCheckInputModel model);

        T GetById<T>(string id);

        T GetActivatedVerificationCode<T>(string userId);

        Task Delete(string id);
    }
}
