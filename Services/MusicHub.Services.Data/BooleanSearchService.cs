namespace MusicHub.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using MusicHub.Data;
    using MusicHub.Data.Common;
    using MusicHub.Data.Models;
    using MusicHub.Filtering;
    using MusicHub.Filtering.Common;
    using MusicHub.Services.Data.Interfaces;
    using MusicHub.Services.Mapping;

    public class BooleanSearchService : IBooleanSearchService
    {
        private readonly ApplicationDbContext context;
        private readonly IQueryBuilder queryBuilder;

        public BooleanSearchService(ApplicationDbContext context, IQueryBuilder queryBuilder)
        {
            this.context = context;
            this.queryBuilder = queryBuilder;
        }

        public IList<TModel> Search<TModel, TEntity, TKey>(string query)
            where TModel : class
            where TEntity : Entity<TKey>
        {
            List<TKey> keys = GetKeys<TEntity, TKey>(query);

            DbSet<TEntity> dbSet = context.Set<TEntity>();
            var records = dbSet
                .Where(e => keys.Contains(e.Id))
                .AsNoTracking()
                .To<TModel>()
                .ToList();
           // var result = new TModel();
            return records;
        }

        public IList<TModel> SearchUsers<TModel>(string query)
            where TModel : class
        {
            List<string> keys = GetKeys<User, string>(query);

            var records = context.Users
                .Where(e => keys.Contains(e.Id))
                .AsNoTracking()
                .To<TModel>()
                .ToList();

            return records;
        }

        private List<TKey> GetKeys<TEntity, TKey>(string query) where TEntity : class
        {
            QueryMetadata queryMetadata = queryBuilder.BuildQuery(query, typeof(TEntity));

            (string sqlQuery, IList<SqlParameter> sqlParameters) = ParameterizeQuery(queryMetadata);
            var keys = new List<TKey>();

            using (DbCommand command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = sqlQuery;
                command.Parameters.AddRange(sqlParameters.ToArray());

                context.Database.OpenConnection();
                using (DbDataReader result = command.ExecuteReader())
                {
                    while (result.Read())
                    {
                        object id = result.GetValue(0);
                        var key = (TKey)Convert.ChangeType(id, typeof(TKey));
                        keys.Add(key);
                    }
                }
            }

            return keys;
        }

        private (string query, IList<SqlParameter> sqlParameters) ParameterizeQuery(QueryMetadata queryMetadata)
        {
            var sqlParameters = new List<SqlParameter>();
            var parameterNames = new List<string>();
            int index = 1;

            foreach (object parameter in queryMetadata.Parameters)
            {
                string parameterName = "@p" + index++;
                parameterNames.Add(parameterName);
                var sqlParameter = new SqlParameter(parameterName, parameter);
                sqlParameters.Add(sqlParameter);
            }

            string sqlQuery = string.Format(queryMetadata.Content, parameterNames.ToArray());
            return (sqlQuery, sqlParameters);
        }
    }
}
