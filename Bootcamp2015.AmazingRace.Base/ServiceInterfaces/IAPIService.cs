using Bootcamp2015.AmazingRace.Base.APIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp2015.AmazingRace.Base.ServiceInterfaces
{
    public interface IAPIService
    {
        Task SignupForTeam(string TeamID);
        Task<LeaderboardValues> getLeaderBoards();
    }
}
