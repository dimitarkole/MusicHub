namespace MusicHub.Common.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class EnumerableExtensions
    {
        public static IQueryable<T> GetPage<T>(this IQueryable<T> collection, int page, int entitesPerPage) =>
            collection
            .Skip(CalculateEntitesToSkip(page, entitesPerPage))
            .Take(entitesPerPage);

        public static IEnumerable<T> GetPage<T>(this IEnumerable<T> collection, int page, int entitesPerPage) =>
            collection
            .Skip(CalculateEntitesToSkip(page, entitesPerPage))
            .Take(entitesPerPage);

        public static int CalculateEntitesToSkip(int page, int entitesPerPage) => (page - 1) * entitesPerPage;
    }
}
