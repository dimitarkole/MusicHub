using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Web.ViewModels.UserLoginHistoryModels
{
    public class UserLoginHistoryFilterInputModel
    {
        public DateTime? LoginDateFrom { get; set; }

        public DateTime? LoginDateTo { get; set; }
    }
}
