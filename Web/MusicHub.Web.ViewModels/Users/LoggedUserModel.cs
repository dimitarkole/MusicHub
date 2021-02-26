using System.Collections.Generic;

namespace CaseManager.Models.Users
{
    public class LoggedUserModel
    {
        public string UserId { get; set; }
        
        public string Username { get; set; }

        public IEnumerable<string> Roles { get; set; }
        
        public string Token { get; set; }
    }
}
