namespace MusicHub.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class BaseFile
    {

        public string Type { get; set; }

        public string Name { get; set; }
    }
}
