namespace MusicHub.Web.Infrastucture.Configurations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MusicHub.Web.Infrastucture.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApiBehaviorConfiguration
    {
        public static IServiceCollection ConfigureApiBehavior(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    return new BadRequestObjectResult(actionContext.ModelState.Errors());
                };
            });

            return services;
        }
    }
}
