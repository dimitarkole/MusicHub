namespace MusicHub.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MusicHub.Common;
    using MusicHub.Data;
    using MusicHub.Data.Models;
    using MusicHub.Services.Data.Interfaces;
    using MusicHub.Services.Interfaces;
    using MusicHub.Services.Mapping;
    using MusicHub.Web.ViewModels.Users;
    using MusicHub.Web.ViewModels.VerificationCodeModels;

    using static MusicHub.Common.ModelConstants;

    public class VerificationCodeService : IVerificationCodeService
    {
        private readonly ApplicationDbContext context;
        private readonly IPaginationService paginationService;

        public VerificationCodeService(
            ApplicationDbContext context,
            IPaginationService paginationService)
        {
            this.context = context;
            this.paginationService = paginationService;
        }

        public bool IsVerificationCodeCorrect(string userId, VerificationCodeCheckInputModel model)
        {
            var verificationCode = this.GetActivatedVerificationCode<VerificationCodeViewModel>(userId);
            return verificationCode == null ? false
                : verificationCode.Code == model.Code
                    && verificationCode.IsUsed == false
                    && verificationCode.Type == model.Type;
        }

        public async Task Create(VerificationCodeCreateInputModel model)
        {
            var verificationCode = model.To<VerificationCode>();
            verificationCode.IsUsed = false;
            await this.context.VerificationCodes.AddAsync(verificationCode);
            await this.context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var verificationCode = this.context.VerificationCodes.Find(id);
            this.context.VerificationCodes.Remove(verificationCode);
            await this.context.SaveChangesAsync();
        }

        public T GetActivatedVerificationCode<T>(string userId)
            => this.context.VerificationCodes
                .Where(vc => vc.UserId == userId && vc.IsUsed == false)
                .OrderByDescending(vc => vc.CreatedOn)
                .To<T>()
                .FirstOrDefault();

        public T GetActivatedVerificationCode<T>(string userId, VerificationType type)
            => this.context.VerificationCodes
                .Where(vc => vc.UserId == userId
                    && vc.Type == type
                    && vc.IsUsed == false)
                .OrderByDescending(vc => vc.CreatedOn)
                .To<T>()
                .FirstOrDefault();

        public T GetById<T>(string id)
            => this.context.VerificationCodes
                .Where(vc => vc.Id == id)
                .To<T>()
                .FirstOrDefault();

        public async Task SetVerificationCodeIsUsed(string userId, string code)
        {
            var verificationCode = this.context.VerificationCodes
                .Where(vc => vc.UserId == userId
                    && vc.Code == code)
                .OrderByDescending(vc => vc.CreatedOn)
                .FirstOrDefault();
            verificationCode.IsUsed = true;
            this.context.VerificationCodes.Update(verificationCode);
            await this.context.SaveChangesAsync();
        }
    }
}
