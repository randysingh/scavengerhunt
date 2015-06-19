using Bootcamp2015.AmazingRace.Base.Models;
using Bootcamp2015.AmazingRace.Base.ServiceInterfaces;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp2015.AmazingRace.Base.Services
{
    public class DataService : IDataService
    {
        private IMobileServiceClient _serviceClient;

        public IMobileServiceClient MobileServiceClient() {
            if (_serviceClient == null)
            {
                _serviceClient = new MobileServiceClient(
                   Connections.MobileServicesUri,
                   Connections.MobileServicesAppKey
               );
            }

            return _serviceClient;
        }

        #region Race

        public Task<IEnumerable<Race>> GetRacesAsync()
        {
            return _serviceClient.InvokeApiAsync<IEnumerable<Race>>("api/race", HttpMethod.Get, null);
        }

        public Task<Race> GetRaceAsync(string raceId)
        {
            return _serviceClient.InvokeApiAsync<Race>(
                String.Format("api/race/{0}", raceId), HttpMethod.Get, null);
        }

        public Task<Team> GetTeamAsync(string raceId, string teamId, int index)
        {
            return _serviceClient.InvokeApiAsync<Team>(
                String.Format("api/race/{0}/team/{1}?skip={2}", raceId, teamId, index),
                HttpMethod.Get, null);
        }

        public Task<IEnumerable<Clue>> GetCluesAsync(string raceId)
        {
            return _serviceClient.InvokeApiAsync<IEnumerable<Clue>>(
                String.Format("api/race/{0}/clues", raceId), HttpMethod.Get, null);
        }

        #endregion

        #region Team

        public Task<IEnumerable<Team>> GetTeamAsync(string raceId)
        {
            return _serviceClient.InvokeApiAsync<IEnumerable<Team>>("api/teams", HttpMethod.Get, null);
        }

        public void UpdateLocationAsync(string raceId, string latitude, string longitude)
        {
            _serviceClient.InvokeApiAsync("api/updatelocation", HttpMethod.Post,
                new Dictionary<string, string>() { 
                    { "raceId", raceId },
                    { "latitude", latitude },
                    { "longitude", longitude }
                });
        }

        #endregion

        #region Clue

        public Task<Clue> GetClueAsync(string clueId)
        {
            return _serviceClient.InvokeApiAsync<Clue>(
                String.Format("api/clue/{0}", clueId), HttpMethod.Get, null);
        }

        public void SubmitClueResponseAsync(string clueId, byte[] imageBytes, string latitude, string longitude)
        {
            _serviceClient.InvokeApiAsync("api/clue", HttpMethod.Post,
                new Dictionary<string, string>() { 
                    { "clueId", clueId },
                    { "data", imageBytes.ToString() },
                    { "latitude", latitude },
                    { "longitude", longitude }
                });
        }

        #endregion

        #region Profile

        public Task<Profile> GetProfileAsync()
        {
            return _serviceClient.InvokeApiAsync<Profile>("api/profile", HttpMethod.Get, null);
        }

        public Task<Team> JoinTeamAsync(string teamCode)
        {
            return _serviceClient.InvokeApiAsync<Team>(
                String.Format("api/profile?joinCode={0}", teamCode), HttpMethod.Post, null);
        }

        #endregion
    }
}
