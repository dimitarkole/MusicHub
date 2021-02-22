using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Data.Models
{
    public class UserRole : IdentityUserRole<string>
    {
        public Role Role { get; set; }

        public User User { get; set; }
    }
}
