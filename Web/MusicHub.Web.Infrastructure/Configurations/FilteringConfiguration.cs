namespace MusicHub.Web.Infrastucture.Configurations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Extensions.DependencyInjection;
    using MusicHub.Data.Common;
    using MusicHub.Filtering;
    using MusicHub.Filtering.Operators;

    public static class FilteringConfiguration
    {
        public static IServiceCollection ConfigureFiltering(this IServiceCollection services) =>
            services.AddSingleton<IQueryBuilder, QueryBuilder>(_ => new QueryBuilder(OperatorsParser.Initialize(), typeof(Converters)));
    }
}
