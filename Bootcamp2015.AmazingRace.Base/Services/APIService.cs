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

        public async Task<joinTeamValues> SignupForTeam(string TeamID)
        {
            var joinedCode = await _mobileServiceClient.InvokeApiAsync<joinTeamValues>("Profile", HttpMethod.Post, new Dictionary<string, string>() { 
            {
                "joinCode",TeamID  
            }
            });
            return joinedCode;
        }
    }
}
