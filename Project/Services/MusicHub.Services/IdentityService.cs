namespace MusicHub.Services
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;

    using MusicHub.Common;
    using MusicHub.Data;
    using MusicHub.Data.Models;
    using MusicHub.Services.Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.IdentityModel.Tokens;
    using NHibernate.Criterion;

    public class IdentityService : IIdentityService
    {
        private readonly ApplicationDbContext context;

        public IdentityService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public string GenerateJwtToken(string userId, string userName, string sicret)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(sicret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Name, userName),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encryptToken = tokenHandler.WriteToken(token);
            return encryptToken;
        }

        public IdentityUserRole<string> SetUserRole(ApplicationUser user)
        {
            this.GenerateRoles();
            var roleName = this.context.Users.Count() == 0 ?
                GlobalConstants.Roles.AdministratorRoleName : GlobalConstants.Roles.UserRoleName;
            var currectRole = this.context.Roles
                .Where(r => r.Name == roleName)
                .FirstOrDefault();

            var userRole = new IdentityUserRole<string>()
            {
                RoleId = currectRole.Id,
                UserId = user.Id,
            };
            return userRole;
        }

        private void GenerateRoles()
        {
            this.GenerateRole(GlobalConstants.Roles.AdministratorRoleName);
            this.GenerateRole(GlobalConstants.Roles.UserRoleName);
        }

        private async void GenerateRole(string roleName)
        {
            if (this.context.Roles
                .FirstOrDefault(r => r.Name == roleName) == null)
            {
                var role = new ApplicationRole(roleName);
                await this.context.Roles.AddAsync(role);
                this.context.SaveChanges();
            }
        }
    }
}
