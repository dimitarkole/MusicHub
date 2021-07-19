using MusicHub.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHub.Services.Data.Interfaces
{
    public interface IBooleanSearchService
    {
        IList<TModel> Search<TModel, TEntity, TKey>(string query)
            where TModel : class
            where TEntity : Entity<TKey>;

        IList<TModel> SearchUsers<TModel>(string query)
            where TModel : class;
    }
}
