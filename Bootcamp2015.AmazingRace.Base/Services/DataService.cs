using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Bootcamp2015.AmazingRace.Base.Models;
using Bootcamp2015.AmazingRace.Base.ServiceInterfaces;

namespace Bootcamp2015.AmazingRace.Base.Services
{
    public class DataService : IDataService
    {
        private readonly IMobileService mobileService;

        public DataService(IMobileService mobileService)
        {
            this.mobileService = mobileService;
        }

        public Task<Profile> GetProfile()
        {
            return this.mobileService.ServiceClient.InvokeApiAsync<Profile>("profile", HttpMethod.Get,
                new Dictionary<string, string>());
        }

        public Task<Team> PostJoinTeam(string joinCode)
        {
            return this.mobileService.ServiceClient.InvokeApiAsync<Team>("profile", HttpMethod.Post,
                   new Dictionary<string, string>(){{"joinCode", joinCode}});
        }

        public Task<Race> GetRace(string id)
        {
            return this.mobileService.ServiceClient.InvokeApiAsync<Race>("race/{id}", HttpMethod.Get,
                   new Dictionary<string, string>(){{"id", id}});
        }

        public Task<IEnumerable<Race>> GetRaceList()
        {
            return this.mobileService.ServiceClient.InvokeApiAsync<IEnumerable<Race>>("race", HttpMethod.Get,
                   new Dictionary<string, string>());
        }

        public Task<IEnumerable<Clue>> GetClueList(string raceId)
        {
            return this.mobileService.ServiceClient.InvokeApiAsync<IEnumerable<Clue>>("race/{raceId}/clues", HttpMethod.Get,
                   new Dictionary<string, string>() { { "raceId", raceId } });
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