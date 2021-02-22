namespace MusicHub.Web.Infrastucture.Configurations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http.Extensions;
    using Microsoft.Extensions.DependencyInjection;

    public static class FilteringConfiguration
    {
       /* public static IServiceCollection ConfigureFiltering(this IServiceCollection services) =>
            services.AddSingleton<IQueryBuilder, QueryBuilder>(_ => new QueryBuilder(OperatorsParser.Initialize(), typeof(Converters)));*/
    }
}
