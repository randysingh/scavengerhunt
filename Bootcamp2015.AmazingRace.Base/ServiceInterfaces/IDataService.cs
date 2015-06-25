using Bootcamp2015.AmazingRace.Base.Models;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp2015.AmazingRace.Base.ServiceInterfaces
{
    public interface IDataService
    {
        IMobileServiceClient MobileServiceClient();

        Task<IEnumerable<Race>> GetRacesAsync();

        Task<Race> GetRaceAsync(string raceId);

        Task<Team> GetTeamAsync(string raceId, string teamId, int index);

        Task<IEnumerable<Clue>> GetCluesAsync(string raceId);

        Task<IEnumerable<Team>> GetTeamAsync(string raceId);

        void UpdateLocationAsync(string raceId, string latitude, string longitude);

        Task<Clue> GetClueAsync(string clueId);

        void SubmitClueResponseAsync(string clueId, byte[] imageBytes, string latitude, string longitude);

        Task<Profile> GetProfileAsync();

        Task<Team> JoinTeamAsync(string teamCode);
    }
}
