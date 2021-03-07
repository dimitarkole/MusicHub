using MusicHub.Common.Settings;
using MusicHub.Data;
using MusicHub.Services.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicHub.Web.ViewModels.Users;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices;

namespace MusicHub.Services.Data
{
    public class ActiveDirectoryService : IActiveDirectoryService
    {
        private readonly ApplicationDbContext context;
        private readonly ActiveDirectorySettings settings;

        public ActiveDirectoryService(ApplicationDbContext context, ActiveDirectorySettings settings)
        {
            this.context = context;
            this.settings = settings;
        }


       /* public List<ActiveDirectoryUserViewModel> All()
        {
            var users = new List<ActiveDirectoryUserViewModel>();
            using (var context = new PrincipalContext(ContextType.Domain, settings.Domain, "OU=Users (o365 Synced),DC=musichub,DC=local", settings.UserName, settings.Password))
            {
                using (var searcher = new PrincipalSearcher(new UserPrincipal(context)))
                {
                    foreach (Principal result in searcher.FindAll())
                    {
                        var adUser = new ActiveDirectoryUserViewModel();
                        var directoryEntry = result.GetUnderlyingObject() as DirectoryEntry;
                        adUser.Email = directoryEntry.Properties["userPrincipalName"].Value.ToString();
                        adUser.Username = directoryEntry.Properties["samAccountName"].Value.ToString();
                        adUser.FullName = directoryEntry.Properties["Name"].Value.ToString();

                        users.Add(adUser);
                    }

                    return users;
                }
            }

        }*/

        public ActiveDirectoryUserViewModel GetByEmail(string email)
        {
            var ctx = new PrincipalContext(ContextType.Domain, settings.Domain);
            var user = UserPrincipal.FindByIdentity(ctx, email);
            if (user != null)
            {
                var adUser = new ActiveDirectoryUserViewModel
                {
                    Email = user.UserPrincipalName,
                    Username = user.SamAccountName,
                    FullName = user.Name
                };

                return adUser;
            }

            return null;
        }

        public bool ValidateCredentials(string username, string password)
        {
            using (var context = new PrincipalContext(ContextType.Domain, settings.Domain, username, password))
            {
                return context.ValidateCredentials(username, password);
            }
        }
    }
}
