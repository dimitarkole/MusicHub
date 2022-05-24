namespace MusicHub.Web.Infrastucture.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal user) =>
           user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
    }
}
