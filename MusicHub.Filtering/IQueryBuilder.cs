using MusicHub.Filtering.Common;
using System;

namespace MusicHub.Filtering
{
    public interface IQueryBuilder
    {
        QueryMetadata BuildQuery(string searchString, Type entityType);
    }
}
