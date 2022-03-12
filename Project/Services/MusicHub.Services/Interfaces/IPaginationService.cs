using System;
using System.Collections.Generic;
using System.Text;

namespace MusicHub.Services.Interfaces
{
    public interface IPaginationService
    {
        int CalculatePagesCount(int elementsCount, int elementsPerPage);
    }
}
