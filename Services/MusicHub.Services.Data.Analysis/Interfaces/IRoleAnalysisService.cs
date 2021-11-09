using MusicHub.Web.ViewModels.ChartModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHub.Services.Data.Analysis.Interfaces
{
    public interface IRoleAnalysisService
    {
        PieChartViewModel GetRolesByCountOfPermissions();

        PieChartViewModel GetRolesByCountOfUsers();

        LineChartViewModel GetRolesByUsersForMonths(ChartFilterData filter);

        LineChartViewModel GetRolesByUsersLastWeek();

        LineChartViewModel GetRolesByAverageCountOfUsersForWeek(ChartFilterData filter);
    }
}
