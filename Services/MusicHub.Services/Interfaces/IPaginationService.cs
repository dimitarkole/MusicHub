namespace MusicHub.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IPaginationService
    {
        int CalculatePagesCount(int elementsCount, int elementsPerPage);
    }
}
