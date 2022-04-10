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
        [Display(Name = "Order by enity username asc")]
        UserNameAsc = 4,
        [Display(Name = "Order by enity username desc")]
        UserNameDesc = 5,
        [Display(Name = "Order by enity email asc")]
        EmailAsc = 6,
        [Display(Name = "Order by enity email desc")]
        EmailDesc = 7,
        [Display(Name = "Best order")]
        BestOrder = 8,
        [Display(Name = "Entities whose are not seen")]
        UnseenFirst = 9,
        [Display(Name = "Entities whose are seen")]
        SeenFirst = 10,
        [Display(Name = "Order enitys by price asc")]
        PriceAsc = 6,
        [Display(Name = "Order enitys by price desc")]
        PriceDesc = 7,
    }
}
