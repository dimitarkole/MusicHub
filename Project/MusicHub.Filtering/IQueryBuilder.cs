using MusicHub.Filtering.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Filtering
{
    public interface IQueryBuilder
    {
        QueryMetadata BuildQuery(string searchString, Type entityType);
    }
}
