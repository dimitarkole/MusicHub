namespace MusicHub.Web.Extensions.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    using MusicHub.Data.Models;
    using MusicHub.Web.Infrastucture.Configurations;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.IdentityModel.Tokens;
    using MusicHub.Web.ViewModels.Users;
    using MusicHub.Services.Data.Interfaces;
    using MusicHub.Common.Settings;
    using static MusicHub.Common.ModelConstants;
    using MusicHub.Services.Data;
    using MusicHub.Web.ViewModels.PermissionModels;
    using MusicHub.Data;

    public static class UserManagerExtensions
    {
        public static async Task<string> Authenticate(this UserManager<User> userManager, string username, string password, JwtSettings settings)
        {
            User user = await userManager.FindByNameAsync(username);
            IEnumerable<string> roles = await userManager.GetRolesAsync(user);
            if (user == null)
            {
                return null;
            }

            bool isPasswordValid = await userManager.CheckPasswordAsync(user, password);
            if (!isPasswordValid)
            {
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(settings.Secret);

            var subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Email, user.Email),
                });

            foreach (string role in roles)
            {
                subject.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            var tokenDiscriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDiscriptor);
            string securityToken = tokenHandler.WriteToken(token);
            return securityToken;
        }

        public static async Task<LoggedUserModel> Authenticate(
            this UserManager<User> userManager,
            IActiveDirectoryService activeDirectoryService,
            string username,
            string password,
            AppSettings settings)
        {
            User user = await userManager.FindByNameAsync(username);

            if (user == null)
            {
                return null;
            }

            if (!await ValidateCredentials(userManager, activeDirectoryService, username, password, user))
            {
                return null;
            }

            IEnumerable<string> roles = await userManager.GetRolesAsync(user);
            var tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(settings.Key);

            var subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email)
            });

            foreach (string role in roles)
            {
                subject.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            var tokenDiscriptor = new SecurityTokenDescriptor
            {
                //Issuer = settings.SiteUrl,
                //Audience = settings.SiteUrl,
                Subject = subject,
                Expires = DateTime.UtcNow.AddDays(AuthtneticationConstants.TokenExpirationTimeInDays),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDiscriptor);
            string securityToken = tokenHandler.WriteToken(token);

            return new LoggedUserModel
            {
                Token = securityToken,
                Username = user.UserName,
                UserId = user.Id,
                Roles = roles.ToList()
            };
        }

        private static async Task<bool> ValidateCredentials(UserManager<User> userManager, IActiveDirectoryService activeDirectoryService, string username, string password, User user)
            => activeDirectoryService.ValidateCredentials(username, password);
    }
}
