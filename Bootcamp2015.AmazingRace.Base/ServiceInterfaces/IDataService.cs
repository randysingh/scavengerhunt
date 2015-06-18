using Bootcamp2015.AmazingRace.Base.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp2015.AmazingRace.Base.ServiceInterfaces
{
    public interface IDataService
    {
        Task<Profile> GetProfileAsync();

        Task<Team> JoinTeamAsync(string teamCode);

        Task<IEnumerable<Race>> GetRacesAsync();

        Task<Clue> GetClueAsync(string raceId, string teamId);

        Task<Clue> GetClueAsync(string raceId, string teamId, int index);

        void PostClueResponse(Clue clue, byte[] imageBytes);
    }
}
