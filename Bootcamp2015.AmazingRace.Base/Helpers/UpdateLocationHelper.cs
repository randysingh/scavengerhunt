using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Security.Credentials;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Bootcamp2015.AmazingRace.Base.Models;
using Bootcamp2015.AmazingRace.Base.ServiceInterfaces;
using Bootcamp2015.AmazingRace.Base.Services;
using Caliburn.Micro;
using Microsoft.WindowsAzure.MobileServices;

namespace Bootcamp2015.AmazingRace.Base.Helpers
{
    public static class UpdateLocationHelper
    {
        private static Geolocator locator = new Geolocator() { ReportInterval = 1000 };


        public async static Task UpdateLocation()
        {
            var pos = await locator.GetGeopositionAsync();
            UpdateMyLocation(pos);
        }

        private async static void UpdateMyLocation(Geoposition p)
        {
            var pos = new Geopoint(p.Coordinate.Point.Position);

            var mobileServiceUser = PasswordVaultHelper.RetriveGoogleUser();
            var mobileServiceClinet = new MobileServiceClient(ApplicationConstants.MobileServicesUri, ApplicationConstants.MobileServicesAppKey);
            mobileServiceClinet.CurrentUser = mobileServiceUser;

            //var races = await mobileServiceClinet.InvokeApiAsync<IEnumerable<Race>>("race", HttpMethod.Get, new Dictionary<string, string>());

            //var race = races.FirstOrDefault();
            //if (race != null)
            //{
            var a = await
                mobileServiceClinet.InvokeApiAsync<Profile>("updatelocation", HttpMethod.Post,
                    new Dictionary<string, string>()
                        {
                            { "raceId", "07FC520D-CD15-4EB7-BB21-792BA5195A5E" }, 
                            { "latitude", pos.Position.Latitude.ToString() }, 
                            { "longitude", pos.Position.Longitude.ToString() }
                        });
            //}
        }
    }
}