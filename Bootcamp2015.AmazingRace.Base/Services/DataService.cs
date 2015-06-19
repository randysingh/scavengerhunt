using System.Collections.Generic;
using System.Threading.Tasks;
using Bootcamp2015.AmazingRace.Base.Models;
using Bootcamp2015.AmazingRace.Base.ServiceInterfaces;

namespace Bootcamp2015.AmazingRace.Base.Services
{
    public class DataService : IDataService
    {
        //MobileService dependency

        public Task<Profile> GetProfile()
        {
            throw new System.NotImplementedException();
        }

        public Task<Team> PostJoinTeam(string joinCode)
        {
            throw new System.NotImplementedException();
        }

        public Task<Race> GetRace(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Clue>> GetClueList(string raceId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Clue> GetClue(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> PostTeamLocation(string raceId, double latitude, double longitude)
        {
            throw new System.NotImplementedException();
        }
    }
}