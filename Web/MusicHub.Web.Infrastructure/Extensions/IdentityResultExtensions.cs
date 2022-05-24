namespace MusicHub.Web.Infrastucture.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public static class IdentityResultExtensions
    {
        public static ActionResult ToActionResult(this IdentityResult identityResult, object responseData = null)
        {
            if (identityResult.Succeeded)
            {
                if (responseData != null)
                {
                    return new OkObjectResult(responseData);
                }
                else
                {
                    return new OkResult();
                }
            }
            else
            {
                return new BadRequestObjectResult(identityResult.Errors.Select(e => e.Description));
            }
        }
    }
}
