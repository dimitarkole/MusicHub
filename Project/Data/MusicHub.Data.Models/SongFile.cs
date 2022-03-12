using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Data.Models
{
    public class SongFile : BaseFile
    {
        public SongFile(string name)
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public Song Parent { get; set; }
    }
}
