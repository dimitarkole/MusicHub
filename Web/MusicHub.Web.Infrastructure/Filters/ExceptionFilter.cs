namespace MusicHub.Web.Infrastucture.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            // TODO: remove the information about the error in the message in production
            string message = $"ErrorMessage: {context.Exception.Message}\n";
            if (context.Exception.InnerException != null)
            {
                message += $"Inner exception message: {context.Exception.InnerException.Message}\n";
                message += $"Inner exception stack trace: {context.Exception.InnerException.StackTrace}\n";
            }

            message += $"Stack trace: {context.Exception.StackTrace}";

            context.Result = new BadRequestObjectResult(message);
        }
    }
}
