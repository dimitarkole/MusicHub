using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Data.Common
{
    public class Entity<TKey>
    {
        public TKey Id { get; set; }
    }
}
