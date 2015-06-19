using Bootcamp2015.AmazingRace.Base.Models;
using Bootcamp2015.AmazingRace.Base.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp2015.AmazingRace.Base.Services
{
    public class DataService : IDataService
    {
        public Task<Profile> GetProfileAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Team> JoinTeamAsync(string teamCode)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Race>> GetRacesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Clue> GetClueAsync(string raceId, string teamId)
        {
            throw new NotImplementedException();
        }

        public Task<Clue> GetClueAsync(string raceId, string teamId, int index)
        {
            throw new NotImplementedException();
        }

        public void PostClueResponse(Clue clue, byte[] imageBytes)
        {
            throw new NotImplementedException();
        }
    }
}
