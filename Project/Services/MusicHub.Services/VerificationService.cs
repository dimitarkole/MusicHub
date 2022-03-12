namespace MusicHub.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MusicHub.Data;
    using MusicHub.Data.Models;
    using MusicHub.Services.Interfaces;
    using MusicHub.Services.Mapping;
    using MusicHub.Web.ViewModels.VerificationCodeModels;

    using static MusicHub.Common.GlobalConstants;

    public class VerificationService : IVerificationService
    {
        private readonly ApplicationDbContext context;

        public VerificationService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public bool Check(VerificationCodeCheckInputModel model)
           => this.context.VerificationCodes
                    .Any(vc => vc.UserId == model.UserId
                            && vc.Code == model.Code
                            && vc.IsUsed == false
                            && (DateTime.UtcNow - vc.CreatedOn).TotalHours <= VerificationCodeSpecification.EndTimeInHours);

        public async Task Create(VerificationCodeCreateInputModel model)
        {
            var verificationCode = model.To<VerificationCode>();
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

        public T GetById<T>(string id)
            => this.context.VerificationCodes
                .Where(vc => vc.Id == id)
                .To<T>()
                .FirstOrDefault();
    }
}
