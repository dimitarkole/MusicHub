namespace MusicHub.Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public enum OrderMethod
    {
        [Display(Name = "Order by enity created on date asc")]
        CreatedOnAsc = 0,
        [Display(Name = "Order by enity created on date desc")]
        CreatedOnDesc = 1,
        [Display(Name = "Order by enity name asc")]
        NameAsc = 2,
        [Display(Name = "Order by enity name desc")]
        NameDesc = 3,
    }
}
