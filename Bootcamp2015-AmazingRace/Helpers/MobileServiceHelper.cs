using Bootcamp2015.AmazingRace.Base;
using Bootcamp2015.AmazingRace.Models;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;

namespace Bootcamp2015.AmazingRace.Helpers
{
    public static class MobileServiceHelper
    {
        private static int _skipCounter;
        private static MobileServiceClient _mobileServiceClient;

        public static MobileServiceClient GetInstance()
        {
            if (_mobileServiceClient == null)
            {
                Init();
            }
            return _mobileServiceClient;
        }

        private static void Init()
        {
            _skipCounter = 0;
            _mobileServiceClient = new MobileServiceClient(Connections.MobileServicesUri, Connections.MobileServicesAppKey);
        }

        public async static void Login()
        {
            var result = await _mobileServiceClient.LoginAsync(MobileServiceAuthenticationProvider.Google);
            _mobileServiceClient.CurrentUser.UserId = result.UserId;
            _mobileServiceClient.CurrentUser.MobileServiceAuthenticationToken = result.MobileServiceAuthenticationToken;
        }

        public static void LoginComplete(WebAuthenticationBrokerContinuationEventArgs args)
        {
            _mobileServiceClient.LoginComplete(args);
        }

        public async static void JoinTeam (String teamCode)
        {
            var paramDictionary = new Dictionary<string, string>();
            paramDictionary.Add("joinCode", teamCode);
            JToken response = await _mobileServiceClient.InvokeApiAsync("profile", HttpMethod.Post, paramDictionary);

        }

        public async static Task<Race> GetFirstRace()
        {
            var races = await GetRaces();
            return races.FirstOrDefault();
        }

        private async static Task<IEnumerable<Race>> GetRaces()
        {
            var races = await _mobileServiceClient.InvokeApiAsync<IEnumerable<Race>>("race", HttpMethod.Get, null);
            return races;
        }

        private async static Task<Profile> GetProfile()
        {
            var profile = await _mobileServiceClient.InvokeApiAsync<Profile>("profile", HttpMethod.Get, null);
            return profile;
        }

        private async static Task<RaceStatus> GetRaceStatus()
        {
            Profile profile = await GetProfile();
            string teamId = profile.teams.First().id;

            string call = string.Format("race/{0}/team/{1}", "test_race", teamId );
            Dictionary<string, string> paramDict = new Dictionary<string, string>();
            paramDict.Add("skip", _skipCounter.ToString());
            var status = await _mobileServiceClient.InvokeApiAsync<RaceStatus>(call, HttpMethod.Get, paramDict);
            return status;
        }

        private async static Task<string> GetNextClueId()
        {
            RaceStatus rs = await GetRaceStatus();
            return rs.nextClueId;
        }
        
        public async static Task<Clue> GetNextClue()
        {
            string cId = await GetNextClueId();
            Clue res = await _mobileServiceClient.InvokeApiAsync<Clue>(string.Format("clue/{0}", cId), HttpMethod.Get, null);
            return res;
        }


         
    }
}
