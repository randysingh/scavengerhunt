using Bootcamp2015.AmazingRace.Base.APIModels;
using Bootcamp2015.AmazingRace.Base.ServiceInterfaces;
using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure.MobileServices;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp2015.AmazingRace.Base.Services
{
    public class APIService : IAPIService
    {
        public MobileServiceClient _mobileServiceClient;
        public APIService()
        {
            _mobileServiceClient = new MobileServiceClient(Connections.MobileServicesUri, Connections.MobileServicesAppKey);
        }

        private string teamid, teamName;
        private string raceId;

        public async Task SignupForTeam(string TeamID)
        {
            joinTeamValues teamValues = await _mobileServiceClient.InvokeApiAsync<joinTeamValues>("Profile", HttpMethod.Post, new Dictionary<string, string>() { 
            {
                "joinCode",TeamID  
            }
            });
            teamid = teamValues.id;
            teamName = teamValues.name;
        }

        public async Task<LeaderboardValues> getLeaderBoards()
        {
            RaceValues raceValues = await _mobileServiceClient.InvokeApiAsync<RaceValues>("Race", HttpMethod.Get, new Dictionary<string, string>());
            string raceId = raceValues.id;

            LeaderboardValues leaderboardValues = await _mobileServiceClient.InvokeApiAsync<LeaderboardValues>("Race/" + raceId, HttpMethod.Get, new Dictionary<string, string>());
            return leaderboardValues;
        }
    }
}
