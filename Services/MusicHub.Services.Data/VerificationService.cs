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
    using MusicHub.Services.Mapping;
    using MusicHub.Web.ViewModels.Users;
    using MusicHub.Web.ViewModels.VerificationCodeModels;

    using static MusicHub.Common.ModelConstants;

    public class VerificationCodeService : IVerificationCodeService
    {
        private readonly ApplicationDbContext context;

        public VerificationCodeService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public bool CheckVerificationCode(string userId, VerificationCodeCheckInputModel model)
            => this.context.VerificationCodes
                   .Any(vc => vc.UserId == userId
                           && vc.Code == model.Code
                           && vc.IsUsed == false
                           && vc.Type == model.Type);
        /*
                           && (DateTime.UtcNow - vc.CreatedOn)
                   .TotalHours <= VerificationCodeConstants.EndTimeInHours);     */

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
    }
}
