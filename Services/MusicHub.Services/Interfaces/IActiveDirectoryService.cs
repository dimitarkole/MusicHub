using MusicHub.Web.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHub.Services.Interfaces
{
     public interface IActiveDirectoryService
    {
        bool ValidateCredentials(string username, string password);

        // List<ActiveDirectoryUserViewModel> All();

        ActiveDirectoryUserViewModel GetByEmail(string email);
    }
}
