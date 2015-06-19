using System.Collections.Generic;
using System.Threading.Tasks;
using Bootcamp2015.AmazingRace.Base.Models;

namespace Bootcamp2015.AmazingRace.Base.ServiceInterfaces
{
    public interface IDataService
    {
        Task<Profile> GetProfile();
        Task<Team> PostJoinTeam(string joinCode);
        Task<Race> GetRace(string id);
        Task<IEnumerable<Clue>> GetClueList(string raceId);
        Task<Clue> GetClue(string id);
        Task<bool> PostTeamLocation(string raceId, double latitude, double longitude);
        //POST CLUE
    }
}